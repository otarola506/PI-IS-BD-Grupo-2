using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace Iteracion_1
{
	public partial class Articulos : System.Web.UI.Page
	{
        private SqlConnection conn;
        Encoding unicode = Encoding.Unicode;

        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            conn = new SqlConnection(conString);
        }
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                getTituloResumen();
            }

        }

        // Get titulo autor y resumen
        protected void getTituloResumen() {

            SqlCommand cmd = null;

            connection();
            conn.Open();

            if (Session["Categoria"] != null || Session["Titulo"] != null)
            {
                if (Session["Categoria"] == null && Session["Titulo"] != null) {
                    cmd = new SqlCommand("RecuperarArtPorTitulo", conn);
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = Session["Titulo"].ToString();

                } else if (Session["Categoria"] != null && Session["Titulo"] == null) {
                    cmd = new SqlCommand("RecuperarArtPorNombreCategoria", conn);
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Session["Categoria"].ToString();
                }
                else {
                    cmd = new SqlCommand("RecuperarArtPorAmbos", conn);
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Session["Categoria"].ToString();
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = Session["Titulo"].ToString();
                }


            }
            else {
                cmd = new SqlCommand("RecuperarArticulosPrincipal", conn);

            }

            cmd.CommandType = CommandType.StoredProcedure;
            

            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            DataTable dTable = new DataTable();

            ad.Fill(dTable);

            
            articlesTable.DataSource = dTable;

            articlesTable.Columns[0].Visible = false;

            articlesTable.DataBind();

            Session["Categoria"] = null;
            Session["Titulo"] = null;

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string resumen = unicode.GetString((byte[])dr["resumen"]);
                (e.Row.FindControl("resumen") as Label).Text = resumen;
            }
        }

        private int retornarValorIdArticulo(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string temp = ((Label)gvr.Cells[0].FindControl("articuloid")).Text;
            int id = Int32.Parse(temp);
            return id;
        }

        public void lnkVerMasArt(object sender, EventArgs e) {
            int idArt = retornarValorIdArticulo(sender);
            Session["ArticuloID"] = idArt;

            Response.Redirect("ArticuloCompleto.aspx");
        }

    }
}