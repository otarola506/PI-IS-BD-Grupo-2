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
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-0UBMGQL; Database=PregRepDB; Integrated Security=true;");
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
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("RecuperarPreguntasFrecuentes", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            gvPregFrec.DataSource = dtbl;
            gvPregFrec.DataBind();
        }

        protected void EnviarPregButton_OnClick(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("AddPreguntaR", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Pregunta", TxtBoxPN.Text.Trim());
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            TxtBoxPN.Text = "";
            FillGridView();
            Response.Write("<script>alert('Pregunta enviada con éxito')</script>");
        }

        protected void VerRespButton_OnClick(object sender, EventArgs e)
        {
            string[] arg = new string[3];
            arg = (sender as Button).CommandArgument.Split(';');
            Session["PregID_SeccionPregFrec"] = arg[0];
            Session["Pregunta_SeccionPregFrec"] = arg[1];
            Session["Respuesta_SeccionPregFrec"] = arg[2];
            Response.Redirect("RespuestasPregMP.aspx");
        }

        protected void BorrarPFButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("BorrarPreguntaFrecuente", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PregID",pregID);
            sqlCmd.ExecuteNonQuery();
            con.Close();
            FillGridView();
            Response.Write("<script>alert('Pregunta frecuente eliminada con éxito')</script>");
        }   
    }
}