using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Iteracion_1
{
    public partial class ArticuloCompleto : System.Web.UI.Page
    {
        private SqlConnection conn;
        Encoding unicode = Encoding.Unicode;
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            conn = new SqlConnection(conString);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getContenidoArticulo();
            }

        }

        private void getContenidoArticulo()
        {
            if (Session["ArticuloID"] != null) {
                int artId = Convert.ToInt32(Session["ArticuloID"]);

                connection();
                conn.Open();

                SqlCommand cmd = new SqlCommand("MostrarArticuloCompleto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                SqlDataReader reader = cmd.ExecuteReader();

                byte[] contenidoByte = new byte[] { };
                string tituloRetorno = "";
                string categorias = "";
                string autor = "";
                if (reader.Read())
                {
                    tituloRetorno = reader[0].ToString();
                    autor = reader[1].ToString();
                    contenidoByte = (byte[])reader[2];

                }
                string contenidoString = unicode.GetString(contenidoByte);

                if (contenidoString.Length != 0)
                {
                    lblTitulo.Text = tituloRetorno;
                    lblAutor.Text = autor;
                    lblContenido.Text = contenidoString;
                    lblCategorias.Text = categorias;
                }
            }
            




        }
    }
}