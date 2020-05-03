using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PreguntasWebForms
{
    public partial class EdicionPreg : System.Web.UI.Page
    {
        private SqlConnection con;

        private void connection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conStr);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GEButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32(Session["PregID"]);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("EditPregunta", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PregID", pregID);
            sqlCmd.Parameters.AddWithValue("@Pregunta", TxtBoxPN.Text.Trim());
            sqlCmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("PreguntasRecibidas.aspx");
        }
    }
}