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
    public partial class EditorArticuloModificado : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            byte[] contenidoByte = new byte[] { };
            byte[] resumenByte = new byte[] { };
            string tituloRetorno = "";
            string tipoArticulo = "";

            if (reader.Read())
            {
                tituloRetorno = reader[1].ToString();
                resumenByte = (byte[])reader[2];
                contenidoByte = (byte[])reader[3];
                tipoArticulo = reader[7].ToString();

            }
            con.Close();
            if (string.Compare(tipoArticulo, "corto") == 0)
            {
                string resumenString = unicode.GetString(resumenByte);
                string contenidoString = unicode.GetString(contenidoByte);
                txtTitulo.Text = tituloRetorno;
                txtResumen.Text = resumenString;
                txtArticulo.Text = contenidoString;
                eliminarCategoria();
            }

        }
        public void cargarCategorias()
        {
            connection();
            SqlCommand cmd = new SqlCommand("Recuperar_Categorias", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            selectCategorias.DataTextField = "nombre";
            //selectCategorias.DataValueField = "categoriaIdPK";
            selectCategorias.DataSource = dt;
            selectCategorias.DataBind();
            con.Close();



        }



        public void ModificarArchivo()
        {
            byte[] bytesText = Seleccionador.FileBytes;
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("Modificar_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("@tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesText;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void ModificarArchivoRevision()
        {
            byte[] bytesText = Seleccionador.FileBytes;
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("Modificar_Articulo_Y_Revision", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("@tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesText;
            cmd.Parameters.Add("@estadoNuevo", SqlDbType.VarChar).Value = "revision";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void eliminarCategoria()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("Borrar_Categoria", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void ModificarArticulosTexto()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("Modificar_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            byte[] bytesText = unicode.GetBytes(txtArticulo.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("@tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesText;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ModificarArticulosTextoRevision()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("Modificar_Articulo_Y_Revision", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            byte[] bytesText = unicode.GetBytes(txtArticulo.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("@tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenidoNuevo", SqlDbType.VarBinary).Value = bytesText;
            cmd.Parameters.Add("@estadoNuevo", SqlDbType.VarChar).Value = "revision";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void guardarCategoriaArticulo(SqlConnection con, string catID, string artID)
        {
            SqlCommand cmd4 = new SqlCommand("Guardar_Catergoria_Articulo", con);
            cmd4.CommandType = CommandType.StoredProcedure;
            int art = Int32.Parse(artID);
            int cat = Int32.Parse(catID);
            cmd4.Parameters.Add("@artId", SqlDbType.Int).Value = art;
            cmd4.Parameters.Add("@catId", SqlDbType.Int).Value = cat;
            cmd4.ExecuteNonQuery();
        }

        public bool checkCategoria()
        {
            bool valor = false;
            foreach (ListItem li in selectCategorias.Items)
            {
                if (li.Selected)
                {
                    valor = true;
                }
            }
            return valor;
        }

        protected void procesoGuardar(bool enviarRevision)
        {
            //revision de categoria 
            bool categoria_seleccionada = checkCategoria();


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
                    else if (!categoria_seleccionada)
                    {
                        lblMensaje.Text = "Por favor seleccione una categoria .";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                        //byte[] bytesText = Seleccionador.FileBytes;
                        string artID = "";


                        connection();
                        if (enviarRevision)
                        {
                            ModificarArchivoRevision();
                        }
                        else
                        {
                            ModificarArchivo(); //revisar
                        }
                        
                        //obtine el artId
                        SqlCommand cmd2 = new SqlCommand("Obtener_articuloId", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
                        SqlDataReader reader = cmd2.ExecuteReader();

                        if (reader.Read())
                        {
                            artID = reader[0].ToString();
                        }





                        //Guardamos las categorias del articulo
                        string categoriasId = "";
                        connection();
                        foreach (ListItem li in selectCategorias.Items)
                        {
                            if (li.Selected) // Si esta seleccionado
                            {
                                //Vamos a recuperar el id del la categoria seleccionada
                                SqlCommand cmd3 = new SqlCommand("Obtener_CategoriaID", con);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Parameters.Add("@nombre", SqlDbType.VarChar).Value = li.Text;
                                con.Open();
                                SqlDataReader lector = cmd3.ExecuteReader();

                                string catID = "";
                                if (lector.Read())
                                {
                                    catID = lector[0].ToString();
                                }
                                lector.Close();
                                // Parte de crear la tupla e insertarla
                                guardarCategoriaArticulo(con, catID, artID);



                                categoriasId = categoriasId + catID; // prueba
                                                                     //lector.Close();
                                con.Close();
                            }
                        }




                        txtTitulo.Text = String.Empty;
                        txtArticulo.Text = String.Empty;

                        //pruebaCategorias.Text = categoriasId;
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
                else if (!categoria_seleccionada)
                {
                    lblMensaje.Text = "Por favor seleccione una categoria .";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string artID = "";
                    //Guardamos el contenido de txtArticulo en  
                    connection();
                    if (enviarRevision)
                    {
                        ModificarArticulosTextoRevision();
                    }
                    else
                    {
                        ModificarArticulosTexto();
                    }
               
                                                                          
                    //obtine el artId
                    SqlCommand cmd2 = new SqlCommand("Obtener_articuloId", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
                    con.Open();
                    SqlDataReader rd = cmd2.ExecuteReader();

                    if (rd.Read())
                    {
                        artID = rd[0].ToString();
                    }





                    //Guardamos las categorias del articulo

                    string categoriasId = "";
                    connection();
                    foreach (ListItem li in selectCategorias.Items)
                    {
                        if (li.Selected) // Si esta seleccionado
                        {
                            //Vamos a recuperar el id del la categoria seleccionada
                            SqlCommand cmd3 = new SqlCommand("Obtener_CategoriaID", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.Add("@nombre", SqlDbType.VarChar).Value = li.Text;
                            con.Open();
                            SqlDataReader lector = cmd3.ExecuteReader();

                            string catID = "";
                            if (lector.Read())
                            {
                                catID = lector[0].ToString();
                            }
                            lector.Close();
                            // Parte de crear la tupla e insertarla
                            guardarCategoriaArticulo(con, catID, artID);



                            categoriasId = categoriasId + catID; // prueba
                            //lector.Close();
                            con.Close();
                        }
                    }



                    txtTitulo.Text = String.Empty;
                    txtResumen.Text = String.Empty;
                    txtArticulo.Text = String.Empty;

                    lblMensaje.Text = "Articulo subido con exito";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("MisArticulos.aspx");

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            procesoGuardar(false);
        }

        protected void btnRevision_Click(object sender, EventArgs e)
        {
            procesoGuardar(true);
        }
        
    }
}