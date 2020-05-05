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
    public partial class SeccionPregFrec : System.Web.UI.Page
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
                FillGridView();
            }
        }

        void FillGridView()
        {
            connection();
            /*if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }*/
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetPreguntasFreg", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            gvPregFrec.DataSource = dtbl;
            gvPregFrec.DataBind();
        }

        protected void EnviarPregButton_OnClick(object sender, EventArgs e)
        {
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("AddPregunta", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            int miembroID = 5;
            sqlCmd.Parameters.AddWithValue("@Pregunta", TxtBoxPN.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@MiembroID", miembroID);
            sqlCmd.Parameters.AddWithValue("@Frecuente", 0);
            sqlCmd.ExecuteNonQuery();
            con.Close();
            TxtBoxPN.Text = "";
            FillGridView();
        }

        protected void VerRespButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["PregID"] = pregID;
            Response.Redirect("RespuestasPreg.aspx");
        }

        protected void RespButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["PregID"] = pregID;
            Response.Redirect("AgregarRespuesta.aspx");
        }
    }
}