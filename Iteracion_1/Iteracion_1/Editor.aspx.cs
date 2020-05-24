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
    public partial class Editor : System.Web.UI.Page
    {

        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conString);
        }

        string conString = "Data Source=172.16.202.75;Initial Catalog=BD_Grupo2;User ID= grupo2;Password=grupo2.";
        private SqlConnection con;
        Encoding unicode = Encoding.Unicode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //The BindHobbies() method is called up in the page load event.
                cargarCategorias();
            }

        }

        public void cargarCategorias()
        {
            SqlConnection con = new SqlConnection(conString);
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

        public void guardarArchivo(SqlConnection cn, string extension_del_archivo)
        {
            SqlCommand cmd = new SqlCommand("GuardarArticulos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            byte[] bytesText = Seleccionador.FileBytes;
            cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumen", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenido", SqlDbType.VarBinary).Value = bytesText;
            cmd.Parameters.Add("@puntuacion", SqlDbType.Float).Value = 0.0;
            cmd.Parameters.Add("@visitas", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@estado", SqlDbType.VarChar).Value = "progreso";
            cmd.Parameters.Add("@tipoArt", SqlDbType.VarChar).Value = "largo";
            //cmd.Parameters.Add("@extencion", SqlDbType.VarChar).Value = extension_del_archivo;
            cn.Open();
            cmd.ExecuteNonQuery();
        }

        public void guardarArticulosTexto(SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand("GuardarArticulos", cn); // Hay que hacer el procedimiento almacenado en la BD de nosotros.
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesText = unicode.GetBytes(txtArticulo.Text);
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumen", SqlDbType.VarBinary).Value = bytesTextResumen;
            cmd.Parameters.Add("@contenido", SqlDbType.VarBinary).Value = bytesText;
            cmd.Parameters.Add("@puntuacion", SqlDbType.Float).Value = 0.0;
            cmd.Parameters.Add("@visitas", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@estado", SqlDbType.VarChar).Value = "progreso";
            cmd.Parameters.Add("@tipoArt", SqlDbType.VarChar).Value = "corto";
            cn.Open();
            cmd.ExecuteNonQuery();
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
        public void guardarMiembrosAutores(SqlConnection con, string miembroID, string artID)
        {
            SqlCommand cmd4 = new SqlCommand("Guardar_Miembro_Articulo", con);
            cmd4.CommandType = CommandType.StoredProcedure;
            int art = Int32.Parse(artID);
            //int mie = Int32.Parse(miembroID);
            cmd4.Parameters.Add("@artId", SqlDbType.Int).Value = art;
            cmd4.Parameters.Add("@miembroId", SqlDbType.VarChar).Value = miembroID;
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

        public bool checkAutores()
        {
            bool valor = true;
            //Separar el string
            string autores_no_separados = txtAutores.Text.ToString();
            lblprueba.Text = autores_no_separados;
            string[] autores = autores_no_separados.Split(',');
            connection();
            con.Open();
            foreach (string autor in autores)
            {
                string miembroID = "";
                SqlCommand cmd2 = new SqlCommand("obtenerMiembroID", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@nombre", SqlDbType.VarChar).Value = autor; // le pasamos el valor del string
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    miembroID = reader[0].ToString();
                }
                reader.Close();

                if (miembroID == "")
                {
                    //El mae metio un nombre de alguien que no pertenece a los miembros
                    valor = false;
                    break;
                }

            }
            con.Close();
            return valor;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
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
                    else if (!checkAutores())
                    {
                        lblMensaje.Text = "Escribio mal el nombre o ingreso un nombre de un miembro que no pertene a la comunidad, solo miembros pueden escribir";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                        //byte[] bytesText = Seleccionador.FileBytes;
                        string artID = "";
                        using (SqlConnection cn = new SqlConnection(conString))
                        {


                            guardarArchivo(cn, extension_del_archivo); //revisar


                            //obtine el artId
                            SqlCommand cmd2 = new SqlCommand("Obtener_articuloId", cn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
                            SqlDataReader reader = cmd2.ExecuteReader();

                            if (reader.Read())
                            {
                                artID = reader[0].ToString();
                            }



                        }

                        //Guardamos las categorias del articulo
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            string categoriasId = "";

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


                        }

                        //Parte de guardar autores 
                        //Separar el string
                        string autores_no_separados = txtAutores.Text.ToString();
                        lblprueba.Text = autores_no_separados;
                        string[] autores = autores_no_separados.Split(',');

                        ////Obtemos el id de cada miembro del arreglo
                        connection();
                        con.Open();
                        foreach (string autor in autores)
                        {
                            string miembroID = "";
                            SqlCommand cmd2 = new SqlCommand("obtenerMiembroID", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@nombre", SqlDbType.VarChar).Value = autor; // le pasamos el valor del string
                            SqlDataReader reader = cmd2.ExecuteReader();

                            if (reader.Read())
                            {
                                miembroID = reader[0].ToString();
                            }
                            reader.Close();


                            //Se encontro al miembro en la tabla
                            //usamos el artID para guardar
                            guardarMiembrosAutores(con, miembroID, artID);

                        }


                        txtTitulo.Text = String.Empty;
                        txtArticulo.Text = String.Empty;

                        //pruebaCategorias.Text = categoriasId;
                        lblMensaje.Text = "Articulo subido con exito";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("MisArticulos.aspx");
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
                else if (!checkAutores())
                {
                    lblMensaje.Text = "Escribio mal el nombre o ingreso un nombre de un miembro que no pertene a la comunidad, solo miembros pueden escribir";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string artID = "";
                    //Guardamos el contenido de txtArticulo en la base
                    using (SqlConnection cn = new SqlConnection(conString))
                    {
                        

                        //PONER EL IF PARA VER SI SE MODIFICA Y SE GUARDA 
                        guardarArticulosTexto(cn);
                        
                        Response.Write("Datos Cargados Correctamente");

                        //obtine el artId
                        SqlCommand cmd2 = new SqlCommand("Obtener_articuloId", cn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@titulo", SqlDbType.VarChar).Value = txtTitulo.Text;
                        SqlDataReader reader = cmd2.ExecuteReader();

                        if (reader.Read())
                        {
                            artID = reader[0].ToString();
                        }



                    }

                    //Guardamos las categorias del articulo
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        string categoriasId = "";

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

                    }


                    //Parte de guardar autores 
                    //Separar el string
                    string autores_no_separados = txtAutores.Text.ToString();
                    lblprueba.Text = autores_no_separados;
                    string[] autores = autores_no_separados.Split(',');

                    ////Obtemos el id de cada miembro del arreglo
                    connection();
                    con.Open();
                    foreach (string autor in autores)
                    {
                        string miembroID = "";
                        SqlCommand cmd2 = new SqlCommand("obtenerMiembroID", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@nombre", SqlDbType.VarChar).Value = autor; // le pasamos el valor del string
                        SqlDataReader reader = cmd2.ExecuteReader();

                        if (reader.Read())
                        {
                            miembroID = reader[0].ToString();
                        }
                        reader.Close();

                        //Se encontro al miembro en la tabla
                        //usamos el artID para guardar
                        guardarMiembrosAutores(con,miembroID,artID);

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


    }
}