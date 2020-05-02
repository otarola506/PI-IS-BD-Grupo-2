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
    public partial class MostrarContenido : System.Web.UI.Page
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

            cargarContenido();
            if (!IsPostBack)
            {
                cargarContenido();

            } 
            
        }

        private void cargarContenido() {
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
            lblTitulo.Text = tituloRetorno;
            lblResumen.Text = resumenString;
            lblArticulo.Text = contenidoString;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisArticulos.aspx");
        }
    }
}