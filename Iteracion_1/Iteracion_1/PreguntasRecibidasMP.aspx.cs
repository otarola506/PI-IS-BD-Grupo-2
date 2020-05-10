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
    public partial class PreguntasRecibidasMP : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-0UBMGQL; Database=PregRepDB; Integrated Security=true;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGridView();
            }
        }

        void FillGridView()
        {
            if(sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetPreguntasRecibidas", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;            
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gvPreguntas.DataSource = dtbl;
            gvPreguntas.DataBind();
        }

        protected void FButton_OnClick(object sender, EventArgs e)
        {
            string[] arg = new string[3];
            arg = (sender as Button).CommandArgument.Split(';');
            string pregunta = Convert.ToString((sender as Button).CommandArgument);
            Session["Pregunta_PreguntasRecibidas"] = pregunta;
            Response.Redirect("AgregarPregFrecMP.aspx");
        }

        protected void DButton_OnClick(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            SqlDataAdapter sqlDa = new SqlDataAdapter("EliminarPreguntaRecibida", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@PregID", pregID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            FillGridView();
            Response.Write("<script>alert('Pregunta eliminada con éxito')</script>");
        }
    }
}