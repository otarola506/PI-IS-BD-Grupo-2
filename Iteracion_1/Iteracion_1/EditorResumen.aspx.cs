using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;

namespace Iteracion_1
{
    public partial class EditorResumen : System.Web.UI.Page
    {
        Encoding unicode = Encoding.Unicode;
        private SqlConnection con;
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conString);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarContenidoArticulo();

            }
        }
        private void cargarContenidoArticulo()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            connection();
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            byte[] resumenByte = new byte[] { };
            string titulo = "";
            if (reader.Read())
            {

                resumenByte = (byte[])reader[2];
                titulo = reader[1].ToString();

            }
            con.Close();

            string resumenString = unicode.GetString(resumenByte);
            txtTitulo.Text = titulo;
            txtResumen.Text = resumenString;


        }

        public void modificarArticuloCompleto(int artId, string estado) {
           
            connection();
            SqlCommand cmd = new SqlCommand("Modificar_Articulo_Largo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            byte[] bytesContenido = subirArchivo.FileBytes;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesContenido;
            cmd.Parameters.Add("@estadoNuevo", SqlDbType.VarChar).Value = estado;
            cmd.Parameters.Add("@nombreArch", SqlDbType.VarChar).Value = subirArchivo.FileName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void modificarTituloResumen(int artId, string estado) {
            connection();
            SqlCommand cmd = new SqlCommand("Modificar_Titulo_Resumen", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@estadoNuevo", SqlDbType.VarChar).Value = estado;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();




        }
        public bool modificarArticuloLargo(string estado)
        {
            bool largo = false;
            int artId = Convert.ToInt32(Session["articuloId"]);
            if (subirArchivo.HasFile)
            {
                string extensionArchivo = System.IO.Path.GetExtension(subirArchivo.FileName);
                if (extensionArchivo != ".doc" && extensionArchivo != ".docx" && extensionArchivo != ".txt" && extensionArchivo != ".pdf")
                {
                    txtError.Text = "Solo se permiten archivos de los tipos doc, docx, pdf y txt.";
                }
                else {
                    modificarArticuloCompleto(artId,estado);
                    largo = true;

                }
                

            }
            else {
                largo = true;
                modificarTituloResumen(artId,estado);
            }
            return largo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text != String.Empty && txtResumen.Text != String.Empty)
            {
                bool check = modificarArticuloLargo("pendiente");
                if (check == true)
                {
                    string Location = "http://localhost:51359/MisArticulos?value1=";
                    var UsuarioActual = Request["value1"];

                    NotificarMiembro(UsuarioActual, txtTitulo.Text);

                    Response.Redirect(Location + UsuarioActual);
                }

            }
            else {
                txtError.Text = "Escriba título y un resumen porfavor";
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            connection();
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            //Descargar archivo desde base de datos
            
            string fileName = reader["nombreArchivo"].ToString();
            byte[] contenidoArt = (byte[])reader["contenido"];
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
            Response.BinaryWrite(contenidoArt);
            Response.Flush();
            Response.End();

        }

        public void NotificarMiembro(string UsuarioActual, string titulo)
        {
            //Primero ocupo encontrar el correo del miembro
            connection();
            string correoDestinatario = "";
            con.Open();
            SqlCommand cmd = new SqlCommand("ObtenerCorreo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = UsuarioActual;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                correoDestinatario = reader[0].ToString();
            }

            reader.Close();
            con.Close();



            MailMessage mm = new MailMessage();
            mm.To.Add(correoDestinatario);
            mm.Subject = "Notificacion de articulo ";
            AlternateView imgview = AlternateView.CreateAlternateViewFromString("Se ha empezado el proceso de revision en el articulo con el titulo:" + titulo
                + "<br/><br/><br/><br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
            var pathName = "~/Imagenes/shieldship.jpg";
            var fileName = Server.MapPath(pathName);
            LinkedResource lr = new LinkedResource(fileName, MediaTypeNames.Image.Jpeg);
            lr.ContentId = "imgpath";
            imgview.LinkedResources.Add(lr);
            mm.AlternateViews.Add(imgview);
            mm.Body = lr.ContentId;
            mm.IsBodyHtml = false;
            mm.From = new MailAddress("comunidadshieldship@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("comunidadshieldship@gmail.com", "BASESdatos176");
            smtp.Send(mm);

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            var UsuarioActual = Request["value1"];
            Response.Redirect("http://localhost:51359/MisArticulos?value1=" + UsuarioActual);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            bool check = modificarArticuloLargo( "progreso");
            
            if (check == true)
            {
                string Location = "http://localhost:51359/MisArticulos?value1=";
                var UsuarioActual = Request["value1"];

                NotificarMiembro(UsuarioActual, txtTitulo.Text);

                Response.Redirect(Location + UsuarioActual);
            }
        }
    }
}