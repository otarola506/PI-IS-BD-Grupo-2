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
            string[] autores = new string[5];



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


            if (cuenta != 0)
            {
                for (int i = 0; i < autores.Length; i++)
                {
                    if (!(autores[i] == null))
                    {
                        aumentarMeritoAutor(autores[i]);
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
            string puntuacion =  "";
            

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

        public void aumentarMeritoAutor( string nombreAutor)
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

            // Ocupo un proc para aumentar el valor del merito del autor
            SqlCommand cmd = new SqlCommand("AumentarMeritoAutor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            cmd.Parameters.Add("@aumento", SqlDbType.Int).Value = 1;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}