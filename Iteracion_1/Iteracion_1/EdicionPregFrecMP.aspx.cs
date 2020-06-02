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

namespace Iteracion_1
{
    public partial class EdicionPregFrecMP : System.Web.UI.Page
    {
        private SqlConnection con;

        private void connection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conStr);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pregunta = Convert.ToString(Session["Pregunta_SeccionPregFrec"]);
                TxtBoxP.Text = pregunta;
                string respuesta = Convert.ToString(Session["Respuesta_SeccionPregFrec"]);
                TxtBoxR.Text = respuesta;
            }
        }

        protected void GCButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32(Session["PregID_SeccionPregFrec"]);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("EditarPreguntaFrecuente", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Pregunta", TxtBoxP.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Respuesta", TxtBoxR.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@PregID", pregID);
            sqlCmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("SeccionPregFrecMP.aspx");
            Response.Write("<script>alert('Respuesta editada con éxito')</script>");
        }

        protected void VolverButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SeccionPregFrecMP.aspx");
        }
    }
}