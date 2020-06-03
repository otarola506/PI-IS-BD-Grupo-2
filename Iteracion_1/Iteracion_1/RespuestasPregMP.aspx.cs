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
    public partial class RespuestasPregMP : System.Web.UI.Page
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
                FillLabels(sender, e);
            }
        }

        void FillLabels(object sender, EventArgs e)
        {
            string pregunta = Convert.ToString(Session["Pregunta_SeccionPregFrec"]);
            string respuesta = Convert.ToString(Session["Respuesta_SeccionPregFrec"]);
            PregLabel.Text = pregunta;
            RespLabel.Text = respuesta;
        }

        protected void VolverButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SeccionPregFrecMP.aspx");
        }

    }
}