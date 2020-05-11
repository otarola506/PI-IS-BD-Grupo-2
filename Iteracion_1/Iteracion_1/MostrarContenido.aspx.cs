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
            connection();
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlDataAdapter ad = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Mostrar_Categorias_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
          
            ad.SelectCommand = cmd;
            ad.Fill(dt);
            grCategorias.DataSource = dt;
            grCategorias.DataBind();
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
            
            while (reader.Read())
            {
                temp += reader["nombre"]+ "<br/>";
            }

            con.Close();

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


            if (reader.Read())
            {
                tituloArt = reader["titulo"].ToString();
                bytesResumen = (byte[])reader["resumen"];
                bytesContenido = (byte[])reader["contenido"];
            }

            resumenArt = unicode.GetString(bytesResumen);
            contenidoArt = unicode.GetString(bytesContenido);
            lblTitulo.Text = tituloArt;
            lblResumen.Text = resumenArt;
            lblContenido.Text = contenidoArt;

            cargarCategorias();
            cargarAutores();
        }
    }
}