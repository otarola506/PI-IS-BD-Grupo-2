using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Iteracion_1
{
	public partial class Articulos : System.Web.UI.Page
	{
        private SqlConnection conn;
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            conn = new SqlConnection(conString);
        }
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                getTituloResumen();
            }

        }

        // Get titulo autor y resumen
        public void getTituloResumen() {
            connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("RecuperarTituloResumen", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            SqlDataReader reader = cmd.ExecuteReader();

            string temp = "";

            while (reader.Read()) {
                temp += "<h1>"+reader["titulo"].ToString()+" Autor:"+ reader["miembroIdFK"].ToString()+"</h1>";
                temp += "<p>"+reader["resumen"].ToString()+"</p>";
                temp += "<br/>";
                temp += "<br/>";
            }

            conn.Close();

            lbl_articulos.Text = temp;
        }
	}
}