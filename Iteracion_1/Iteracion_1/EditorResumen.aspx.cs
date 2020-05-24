using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
namespace Iteracion_1
{
    public partial class EditorResumen : System.Web.UI.Page
    {
        Encoding unicode = Encoding.Unicode;
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
                cargarContenidoArticulo();

            }
        }
        private void cargarContenidoArticulo()
        {
            int artId = Convert.ToInt32(Session["articuloId"]);
            connection();
            SqlCommand cmd = new SqlCommand("RecuperarArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            byte[] resumenByte = new byte[] { };
            string titulo = "";
            if (reader.Read())
            {

                resumenByte = (byte[])reader[2];
                titulo = reader[1].ToString();

            }
            con.Close();

            string resumenString = unicode.GetString(resumenByte);
            txtTitulo.Text = titulo;
            txtResumen.Text = resumenString;


        }
        
        public void ModificarResumenTexto()
        {
            connection();
            int artId = Convert.ToInt32(Session["articuloId"]);
            SqlCommand cmd = new SqlCommand("Modificar_Titulo_Resumen", con);
            cmd.CommandType = CommandType.StoredProcedure;
            byte[] bytesTextResumen = unicode.GetBytes(txtResumen.Text);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = artId;
            cmd.Parameters.Add("tituloNuevo", SqlDbType.VarChar).Value = txtTitulo.Text;
            cmd.Parameters.Add("@resumenNuevo", SqlDbType.VarBinary).Value = bytesTextResumen;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtResumen.Text != String.Empty)
            {
                ModificarResumenTexto();
                Response.Redirect("MisArticulos.aspx");

            }
            else {
                txtError.Text = "Escriba un resumen porfavor";
            }
        }

        
    }
}