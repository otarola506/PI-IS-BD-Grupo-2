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
                FillGridView(sender, e);
            }
        }

        void FillGridView(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32(Session["PregID"]);
            string Pregunta = Convert.ToString(Session["Pregunta"]);
            PregLabel.Text = Pregunta;
            connection();
            /*if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }*/
            con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("GetRespuestasPreg", con);
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
            Response.Redirect("SeccionPregFrecMP.aspx");
        }

        protected void ERButton_OnClick(object sender, EventArgs e)
        {
            int pregID = Convert.ToInt32(Session["PregID"]);
            Session["PregID"] = pregID;
            Response.Redirect("AgregarRespuestaMP.aspx");
        }

        protected void DButton_OnClick(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as Button).CommandArgument.Split(';');
            int pregID = Convert.ToInt32(arg[0]);
            int respID = Convert.ToInt32(arg[1]);
            connection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("EliminarRespuesta", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@RespID", respID);
            sqlDa.SelectCommand.Parameters.AddWithValue("@PregID", pregID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            con.Close();
            FillGridView(sender,e);
            Response.Write("<script>alert('Respuesta eliminada con éxito')</script>");
        }

        protected void EButton_OnClick(object sender, EventArgs e)
        {
            string[] arg = new string[3];
            arg = (sender as Button).CommandArgument.Split(';');
            Session["PregID"] = arg[0];
            Session["Respuesta"] = arg[1];
            Session["RespID"] = arg[2];
            Response.Redirect("EdicionRespMP.aspx");
        }
    }
}