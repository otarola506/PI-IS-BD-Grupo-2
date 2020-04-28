using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Iteracion_1
{
    public partial class MisArticulos : System.Web.UI.Page
    {
        private SqlConnection con;
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conString);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarTabla();
            }
        }
        void llenarTabla()
        {
            connection();
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("Recuperar_ID_Titulo_Resumen", con);
            ad.Fill(dt);
            tablaArticulos.DataSource = dt;
            tablaArticulos.DataBind();
            if (dt.Rows.Count > 0)
            {
                tablaArticulos.DataSource = dt;
                tablaArticulos.DataBind();

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                tablaArticulos.DataSource = dt;
                tablaArticulos.DataBind();
                tablaArticulos.Rows[0].Cells.Clear();
                tablaArticulos.Rows[0].Cells.Add(new TableCell());
                tablaArticulos.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                tablaArticulos.Rows[0].Cells[0].Text = "No se han encontrado datos.";
                tablaArticulos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

            }


        }
        public int retornarValorIdArticulo(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string temp = ((Label)gvr.Cells[0].FindControl("artId")).Text;
            int id = Int32.Parse(temp);
            return id;


        }
        public void lnkEdicion(object sender, EventArgs e)
        {
            int artId = retornarValorIdArticulo(sender);

        }

        public void lnkBorrado(object sender, EventArgs e)
        {
            int artId = retornarValorIdArticulo(sender);
            connection();
            SqlCommand cmd = new SqlCommand("Borrar_Articulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            cmd.ExecuteNonQuery();
            llenarTabla();
            lblExito.Text = "Datos Borrados Correctamente";

        }

        public void lnkVerMasArt(object sender, EventArgs e)
        {
            int artId = retornarValorIdArticulo(sender);


        }






    }
}