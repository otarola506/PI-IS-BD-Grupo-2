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
    public partial class AgregarPregFrecMP : System.Web.UI.Page
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
                string pregunta = Convert.ToString(Session["Pregunta_PreguntasRecibidas"]);
                TxtBoxP.Text = pregunta;
            }
        }

        protected void GRButton_OnClick(object sender, EventArgs e)
        {
            if((TxtBoxP.Text != String.Empty) && (TxtBoxR.Text != String.Empty))
            {
                connection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand sqlCmd = new SqlCommand("GuardarPreguntaFrecuente", con);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Pregunta", TxtBoxP.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Respuesta", TxtBoxR.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Username","Coordinador" );
                sqlCmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Pregunta agregada a seccion de preguntas frecuentes con éxito')</script>");
                Response.Redirect("SeccionPregFrecMP.aspx");
            }
            else
            {
                Response.Write("<script>alert('No puede agregar una pregunta o respuesta vacía. Por favor llene los dos espacios.')</script>");
            }
        }

        protected void VolverButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SeccionPregFrecMP.aspx");
        }
    }
}