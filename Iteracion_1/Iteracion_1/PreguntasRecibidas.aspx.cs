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
    public partial class PreguntasRecibidas : System.Web.UI.Page
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
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetTablaPreguntas", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            gvPreguntas.DataSource = dtbl;
            gvPreguntas.DataBind();
        }

        protected void FButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("EditPreguntaToPF", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@PregID", pregID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
        }

        protected void DButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("EliminarPregunta", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@PregID", pregID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            FillGridView();
        }

        protected void EButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["PregID"] = pregID;
            Response.Redirect("EdicionPreg.aspx");
        }
    }   
}