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
    public partial class SeccionPregFrecMP : System.Web.UI.Page
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
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetPreguntasFrecuentes", con);
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
            sqlCmd.Parameters.AddWithValue("@Pregunta", TxtBoxPN.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@MiembroID", 0);
            sqlCmd.Parameters.AddWithValue("@Frecuente", 0);
            sqlCmd.ExecuteNonQuery();
            con.Close();
            TxtBoxPN.Text = "";
            FillGridView();
        }

        protected void VerRespButton_OnClick(object sender, EventArgs e)
        {
            //int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            string[] arg = new string[2];
            arg = (sender as Button).CommandArgument.Split(';');
            Session["PregID"] = arg[0];
            Session["Pregunta"] = arg[1];
            //Session["PregID"] = pregID;
            Response.Redirect("RespuestasPregMP.aspx");
        }

        protected void EFButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("EliminarPreguntaFromPF", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@PregID", pregID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            FillGridView();
            Response.Write("<script>alert('Pregunta eliminada de la sección de preguntas frecuentes con éxito')</script>");
        }
    }
}