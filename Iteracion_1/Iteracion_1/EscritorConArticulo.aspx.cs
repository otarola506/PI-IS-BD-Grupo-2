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
    public partial class EscritorConArticulo : System.Web.UI.Page
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
                cargarCategorias();
                cargarContenidoArticulo();
            }
        }
        private void cargarContenidoArticulo()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            SqlDataReader reader = cmd.ExecuteReader();
            byte[] contenidoByte = new byte[] { };
            byte[] resumenByte = new byte[] { };
            string tituloRetorno = "";
            if (reader.Read())
            {
                tituloRetorno = reader[1].ToString();
                resumenByte = (byte[])reader[2];
                contenidoByte = (byte[])reader[4];

            }
            string resumenString = unicode.GetString(resumenByte);
            string contenidoString = unicode.GetString(contenidoByte);
            txtTitulo.Text = tituloRetorno;
            txtResumen.Text = resumenString;
            txtArticulo.Text = contenidoString;








        }

        //Create one method "BindHobbies" for populate hobbies from Database
        public void cargarCategorias()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Recuperar_Categorias", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            selectCategorias.DataTextField = "nombre";
            //selectCategorias.DataValueField = "categoriaIdPK";
            selectCategorias.DataSource = dt;
            selectCategorias.DataBind();
            con.Close();



        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Seleccionador.HasFile && txtArticulo.Text == String.Empty)
            {
                string extension_del_archivo = System.IO.Path.GetExtension(Seleccionador.FileName);

                if (extension_del_archivo != ".doc" && extension_del_archivo != ".docx" && extension_del_archivo != ".txt" && extension_del_archivo != ".pdf")
                {
                    lblMensaje.Text = "Solo se permiten archivos de los tipos doc, docx, pdf y txt ";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (txtTitulo.Text == String.Empty)
                    {
                        lblMensaje.Text = "Por favor ingrese un titulo";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (txtResumen.Text == String.Empty)
                    {
                        lblMensaje.Text = "Por favor ingrese el resumen.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        //Guardo los valores de categoria seleccionados en un string
                        foreach (ListItem li in selectCategorias.Items)
                        {
                            if (li.Selected) // Si esta seleccionado
                            {

                            }
                        }

                        byte[] bytesText = Seleccionador.FileBytes;
                        int artId = Convert.ToInt32(Session["articuloId"]);
                        connection();
                        SqlCommand cmd = new SqlCommand("Modificar_Articulo", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
                        cmd.Parameters.Add("@tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
                        cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
                        cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesText;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        txtTitulo.Text = String.Empty;
                        txtResumen.Text = String.Empty;
                        txtArticulo.Text = String.Empty;




                        lblMensaje.Text = "Articulo subido con exito";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                    }

                }

            }
            else if (!(txtArticulo.Text == String.Empty) && !(Seleccionador.HasFile))
            {

                if (txtTitulo.Text == String.Empty)
                {
                    lblMensaje.Text = "Por favor ingrese un titulo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (txtResumen.Text == String.Empty)
                {
                    lblMensaje.Text = "Por favor ingrese el resumen.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //Guardamos el contenido de txtArticulo en la base

                    byte[] bytesText = Seleccionador.FileBytes;
                    int artId = Convert.ToInt32(Session["articuloId"]);
                    connection();
                    SqlCommand cmd = new SqlCommand("Modificar_Articulo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string co = txtArticulo.Text;
                    byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
                    cmd.Parameters.Add("@tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
                    cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
                    cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesText;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    txtTitulo.Text = String.Empty;
                    txtResumen.Text = String.Empty;
                    txtArticulo.Text = String.Empty;


                    lblMensaje.Text = "Articulo subido con exito";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }



            }
            else if (Seleccionador.HasFile && txtArticulo.Text != String.Empty)
            {
                lblMensaje.Text = "Solo se puede o escribir o subir no ambos, intentelo de nuevo :)";
                lblMensaje.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                lblMensaje.Text = "Por favor escriba una articulo o suba un articulo ya escrito";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}