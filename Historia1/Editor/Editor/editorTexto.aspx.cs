using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Editor
{
    public partial class editorTexto : System.Web.UI.Page
    {
        Encoding unicode = Encoding.Unicode;
        //Hay que cambiar la conexión para que sea la base de datos de nosotrs
        string conString =" Data Source=DESKTOP-8DBNQ4T;Initial Catalog = StudentDB; Integrated Security = True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

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
                        byte[] bytesText = Seleccionador.FileBytes; // o

                        using (SqlConnection cn = new SqlConnection(conString))
                        {
                            SqlCommand cmd = new SqlCommand("GuardarArticulos", cn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            
                            cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
                            //cmd.Parameters.Add("@resumen", SqlDbType.VarBinary).Value = txtResumen.Text;
                            cmd.Parameters.Add("@contenido", SqlDbType.VarBinary).Value = bytesText;
                            cn.Open();
                            cmd.ExecuteNonQuery();
                            txtTitulo.Text = String.Empty;
                            txtArticulo.Text = String.Empty;


                        }

                        lblMensaje.Text = "Articulo subido con exito";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                    }

                }

            }else if (!(txtArticulo.Text == String.Empty) && !(Seleccionador.HasFile))
            {
                //Guardamos el contenido de txtArticulo en la base
                using (SqlConnection cn = new SqlConnection(conString))
                {

                    SqlCommand cmd = new SqlCommand("GuardarArticulos", cn); // Hay que hacer el procedimiento almacenado en la BD de nosotros.
                    cmd.CommandType = CommandType.StoredProcedure;
                    byte[] bytesText = unicode.GetBytes(txtArticulo.Text);
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
                    cmd.Parameters.Add("@contenido", SqlDbType.VarBinary).Value = bytesText;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    Response.Write("Datos Cargados Correctamente") ;
                    txtTitulo.Text = String.Empty;
                    txtResumen.Text = String.Empty;
                    txtArticulo.Text = String.Empty;
                }
                
                lblMensaje.Text = "Articulo subido con exito";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
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

        protected void txtResumen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}