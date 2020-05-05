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
    public partial class RespuestasPreg : System.Web.UI.Page
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
                FillGridView(sender, e);
            }
        }

        void FillGridView(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32(Session["PregID"]);
            connection();
            /*if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }*/
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetRespuestas", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@PregID", pregID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            gvRptasPreg.DataSource = dtbl;
            gvRptasPreg.DataBind();
        }

        protected void VolverButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SeccionPregFrec.aspx");
        }

        protected void ERButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32(Session["PregID"]);
            Session["PregID"] = pregID;
            Response.Redirect("AgregarRespuesta.aspx");
        }
    }
}