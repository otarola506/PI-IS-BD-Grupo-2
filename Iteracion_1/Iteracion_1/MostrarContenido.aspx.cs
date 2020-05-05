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


        }
    }
}