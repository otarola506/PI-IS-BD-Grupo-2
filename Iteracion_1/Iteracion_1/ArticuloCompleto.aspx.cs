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
            Page lastPage = (Page)Context.Handler;
            if (!IsPostBack)
            {
                if ( lastPage is Articulos) {
                    string hola = "hola";
                    getContenidoArticulo(lastPage);
                }
            }

        }

        private void getContenidoArticulo(Page lastPage)
        {
            connection();
            conn.Open();

            int artId = Convert.ToInt32(Session["idArt"]);

            string commando = "SELECT A.titulo, M.nombre, C.nombre, A.contenido " +
                               "FROM Articulo A JOIN Art_Categoria AC " +
                                    "ON A.artIdPK = AC.artIdFK " +
                               "JOIN dbo.Miembro M " +
                                    "ON M.miembroIdPK = A.miembroIdFK " +
                               "JOIN Categoria C " +
                                    "ON AC.categoriaIdFK = C.categoriaIdPK " +
                                    "WHERE C.categoriaIdPK ="+ artId;

            SqlCommand cmd = new SqlCommand(commando, conn);
            cmd.CommandType = CommandType.Text;

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
                categorias = reader[2].ToString();
                contenidoByte = (byte[])reader[4];

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