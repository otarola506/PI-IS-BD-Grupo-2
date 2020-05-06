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
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetPreguntasJOINMiembros", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            gvPreguntas.DataSource = dtbl;
            gvPreguntas.DataBind();
        }

        protected void FButton_OnClick(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as Button).CommandArgument.Split(';');
            int pregID = Convert.ToInt32(arg[0]);
            bool frecuente = Convert.ToBoolean(arg[1]);
            if (frecuente == false)
            {
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
                Response.Write("<script>alert('Pregunta agregada a seccion de preguntas frecuentes con éxito')</script>");
            }
            else
            {
                Response.Write("<script>alert('Esta pregunta ya se encuentra en la sección de preguntas frecuentes')</script>");
            }
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
            Response.Write("<script>alert('Pregunta eliminada con éxito')</script>");
        }

        protected void EButton_OnClick(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as Button).CommandArgument.Split(';');
            Session["PregID"] = arg[0];
            Session["Pregunta"] = arg[1];
            Response.Redirect("EdicionPreg.aspx");
        }
    }   
}