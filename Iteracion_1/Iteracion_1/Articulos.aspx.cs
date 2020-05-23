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

            string buscando = "<h1><strong>Mostrando articulos ";

            connection();
            conn.Open();

            if (Session["Categoria"] != null || Session["Titulo"] != null)
            {
                if (Session["Categoria"] == null && Session["Titulo"] != null) { // Buscando por titulo
                    buscando += "con titulo</strong> '" + Session["Titulo"].ToString()+"'</h1>";

                    cmd = new SqlCommand("RecuperarArtPorTitulo", conn);
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = Session["Titulo"].ToString();

                } else if (Session["Categoria"] != null && Session["Titulo"] == null) { //Buscando por categoria
                    buscando += "de la categoria</strong> '" + Session["nombreCategoria"].ToString()+"'</h1>";

                    cmd = new SqlCommand("RecuperarArtPorNombreCategoria", conn);
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(Session["Categoria"]);
                }
                else {
                    buscando += "con titulo</strong> '" + Session["Titulo"].ToString() + "'<strong> de la categoria</strong> '" + Session["nombreCategoria"].ToString()+"'</h1>";
                    cmd = new SqlCommand("RecuperarArtPorAmbos", conn);
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToInt32(Session["Categoria"]);
                    cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = Session["Titulo"].ToString();
                }


            }
            else {
                buscando = "<h1><strong>Mostrando todos los artículos</strong></h1>";
                cmd = new SqlCommand("RecuperarArticulosPrincipal", conn);

            }
            

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            DataTable dTable = new DataTable();
            ad.Fill(dTable);

            if (dTable.Rows.Count != 0)
            {
                articlesTable.DataSource = dTable;
                articlesTable.Columns[0].Visible = false;
                articlesTable.DataBind();
                buscandoLiteral.Text = buscando;
            }
            else
            {
                buscandoLiteral.Text = "<h1> <strong>No se encontraron resultados</strong></h1>";
            }

            

            buscando = "";
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
            ImageButton btn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string temp = ((Label)gvr.Cells[0].FindControl("articuloid")).Text;
            int id = Int32.Parse(temp);
            return id;
        }

        public void lnkVerMasArt(object sender, EventArgs e)
        {
            connection();
            conn.Open();
            int artId = retornarValorIdArticulo(sender);
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            SqlDataReader reader = cmd.ExecuteReader();

            String tipoArticulo = "";
            reader.Read();
            tipoArticulo = (String)reader[7];
            reader.Close();
            if (tipoArticulo == "corto")
            {
                Session["articuloID"] = artId;
                Response.Redirect("MostrarContenido.aspx");
            }
            else   //Descargar archivo desde base de datos
            {
                reader = cmd.ExecuteReader();
                reader.Read();
                string fileName = reader["titulo"].ToString();
                byte[] contenidoArt = (byte[])reader["contenido"];
                string extension = reader[5].ToString();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = extension;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(contenidoArt);
                Response.Flush();
                Response.End();
            }
        }

    }
}