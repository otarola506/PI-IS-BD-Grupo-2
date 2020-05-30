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

        public void modificarArticuloCompleto(int artId) {
           
            connection();
            SqlCommand cmd = new SqlCommand("Modificar_Articulo_Largo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            byte[] bytesContenido = subirArchivo.FileBytes;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesContenido;
            cmd.Parameters.Add("@estadoNuevo", SqlDbType.VarChar).Value = "revision";
            cmd.Parameters.Add("@nombreArch", SqlDbType.VarChar).Value = subirArchivo.FileName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void modificarTituloResumen(int artId) {
            connection();
            SqlCommand cmd = new SqlCommand("Modificar_Titulo_Resumen", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@estadoNuevo", SqlDbType.VarChar).Value = "revision";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();




        }
        public bool modificarArticuloLargo()
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
                    modificarArticuloCompleto(artId);
                    largo = true;

                }
                

            }
            else {
                modificarTituloResumen(artId);
            }
            return largo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text != String.Empty && txtResumen.Text != String.Empty)
            {
                bool check = modificarArticuloLargo();
                if (check == true)
                {
                    Response.Redirect("MisArticulos.aspx");
                }

            }
            else {
                txtError.Text = "Escriba un resumen y un título porfavor";
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

        
    }
}