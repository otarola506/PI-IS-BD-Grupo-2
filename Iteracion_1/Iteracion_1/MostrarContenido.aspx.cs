using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
namespace Iteracion_1
{
    public partial class MostrarContenido : System.Web.UI.Page
    {
        Encoding unicode = Encoding.Unicode;
        private SqlConnection con;
        string[] autores = new string[5];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarContenido();

            }
        }

        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conString);
        }

        private void cargarCategorias()
        {
            string temp = "";
            connection();
            con.Open();
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlDataAdapter ad = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Mostrar_Categoria_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp += reader["nombre"] + "<br/>";
            }

            con.Close();

            lblCategoria.Text = temp;
        }

        private void cargarAutores()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            string temp = "";
            



            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Mostrar_Autores_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@articuloId", SqlDbType.Int).Value = artId;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            SqlDataReader reader = cmd.ExecuteReader();

            int cuenta = 0;
            while (reader.Read())
            {
                temp += reader["nombre"] + "<br/>";
                //Guardo los nombres de los autores 
                string nombreAutor = reader["nombre"].ToString();
                autores[cuenta] = nombreAutor;
                cuenta++;
            }

            con.Close();


            if (cuenta != 0 )
            {
                for (int i = 0; i < autores.Length; i++)
                {
                    if (!(autores[i] == null))
                    {
                        aumentarMeritoAutor(autores[i], 1, con); // aumentamos en 1 el merito
                    }
                    else
                    {
                        break;
                    }
                }
            }


            lblAutores.Text = temp;
        }

        private void cargarContenido()
        {
            connection();

            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            string tituloArt = "";
            byte[] bytesResumen = new byte[] { };
            byte[] bytesContenido = new byte[] { };
            string resumenArt = "";
            string contenidoArt = "";
            // Debo de agregar lo de las visitas y puntuacion
            string visitas = "";
            string puntuacion = "";


            if (reader.Read())
            {
                tituloArt = reader["titulo"].ToString();
                bytesResumen = (byte[])reader["resumen"];
                bytesContenido = (byte[])reader["contenido"];
                // Debo de agregar lo de las visitas y puntuacion
                visitas = reader["visitas"].ToString();
                puntuacion = reader["puntuacion"].ToString();
            }

            resumenArt = unicode.GetString(bytesResumen);
            contenidoArt = unicode.GetString(bytesContenido);
            lblTitulo.Text = tituloArt;
            lblResumen.Text = resumenArt;
            lblContenido.Text = contenidoArt;
            // Debo de agregar lo de las visitas y puntuacion
            lblPuntuacion.Text = puntuacion;
            lblvisitas.Text = visitas;

            cargarCategorias();
            cargarAutores();

            //Hacer un metodo que aumente la visitas del articulo
            aumentarVisitas();


        }

        public void aumentarVisitas()
        {
            //usando el artID aumentamos el valor de las visitas
            int artId = Convert.ToInt32(Session["articuloId"]);
            // Ocupo un proc para aumentar el valor de las visitas
            SqlCommand cmd = new SqlCommand("IncrementarVisitas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@artID", SqlDbType.Int).Value = artId;
            con.Open();
            cmd.ExecuteNonQuery();

        }


        public void cargarAutores(int diferencia)
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            string temp = "";

            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Mostrar_Autores_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@articuloId", SqlDbType.Int).Value = artId;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            SqlDataReader reader = cmd.ExecuteReader();

            int cuenta = 0;
            while (reader.Read())
            {
                //temp += reader["nombre"] + "<br/>";
                //Guardo los nombres de los autores 
                string nombreAutor = reader["nombre"].ToString();
                autores[cuenta] = nombreAutor;
                cuenta++;
            }

            con.Close();
        }

        public void aumentarMeritoAutor(string nombreAutor, int aumento, SqlConnection con)
        {
            string nombreUsuario = "";

            

            //con el nombre del autor buscamos su nombre de usuario
            SqlCommand cmd0 = new SqlCommand("RetornarNombreUsuario", con);
            cmd0.CommandType = CommandType.StoredProcedure;
            cmd0.Parameters.Add("@nombreM", SqlDbType.VarChar).Value = nombreAutor;
            con.Open();
            SqlDataAdapter ad = new SqlDataAdapter(cmd0);
            SqlDataReader reader = cmd0.ExecuteReader();
            if (reader.Read())
            {
                nombreUsuario = reader[0].ToString();
            }
            con.Close();

            //  proc para modificar el valor del merito del autor

            if (aumento > 0)
            {
                SqlCommand cmd = new SqlCommand("AumentarMeritoAutor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
                cmd.Parameters.Add("@aumento", SqlDbType.Int).Value = aumento;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }else if (aumento < 0)
            {
                aumento = aumento * -1;
                SqlCommand cmd = new SqlCommand("DisminuirMeritoAutor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
                cmd.Parameters.Add("@aumento", SqlDbType.Int).Value = aumento;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Ocupo modificar la puntuacion del articulo y los meritos de los autores
            cargarAutores(20);
            //primero revisar que opcion se selecciono
            if (opVotos.SelectedValue == "0")
            {
                //No voto
                //lblMensaje.Text = "No selecciono ninguna de las opciones, por favor vuelva a intentarlo";
                //lblMensaje.ForeColor = System.Drawing.Color.Red;
                //Revisar si vale la pena hacer algo

            }
            else if (opVotos.SelectedValue == "1")
            {
                //Sumamos un punto
                modificarPuntuacionArticulo(1);
                for (int i = 0;i < autores.Length; i++)
                {
                    if (autores[i] != null)
                    {
                        aumentarMeritoAutor(autores[i], 1,con);
                    }
                }

                lblMensaje.Text = "Voto enviado con exito, gracias por participar.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            else if (opVotos.SelectedValue == "2")
            {
                //Restamos un punto
                modificarPuntuacionArticulo(-1);
                
                for (int i = 0; i < autores.Length; i++)
                {
                    string a = autores[i];
                    if (autores[i] != null)
                    {
                        aumentarMeritoAutor(autores[i], -1,con);
                    }
                }

                lblMensaje.Text = "Voto enviado con exito, gracias por participar.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            

            //modificar el valor de los autores 
        }
        

        public void modificarPuntuacionArticulo(int valorModificar)
        {
            connection();
            //usando el artID modificamos el valor de la puntuacion
            int artId = Convert.ToInt32(Session["articuloId"]);
            if (valorModificar == 1)
            {
                SqlCommand cmd = new SqlCommand("ModificarPuntuacion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@artID", SqlDbType.Int).Value = artId;
                cmd.Parameters.Add("@valor", SqlDbType.Int).Value = valorModificar;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


            }
            else if (valorModificar == -1)
            {
                valorModificar = valorModificar * -1;
                SqlCommand cmd = new SqlCommand("DisminuirPuntuacion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@artID", SqlDbType.Int).Value = artId;
                cmd.Parameters.Add("@valor", SqlDbType.Int).Value = valorModificar;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}