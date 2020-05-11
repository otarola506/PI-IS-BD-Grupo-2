using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Iteracion_1
{
    public partial class SiteMaster : MasterPage
    {
        private SqlConnection con;
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["grupo2Conn"].ToString();
            con = new SqlConnection(conString);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            connection();
            con.Open();

            if (TextBox1.Text == String.Empty & DropDownList.SelectedValue == "null")
            {
                Session["Titulo"] = null;
                Session["Categoria"] = null;
            }
            else
            {
                if (TextBox1.Text != String.Empty & DropDownList.SelectedValue == "null")
                {
                    Session["Titulo"] = TextBox1.Text;
                    Session["Categoria"] = null;
                }
                else
                {
                    if (TextBox1.Text == String.Empty & DropDownList.SelectedValue != "null")
                    {
                        Session["Titulo"] = null;
                        Session["Categoria"] = DropDownList.SelectedValue;
                    }
                    else
                    {
                        Session["Titulo"] = TextBox1.Text;
                        Session["Categoria"] = DropDownList.SelectedValue;
                        
                    }
                }
            }

            Session["nombreCategoria"] = DropDownList.SelectedItem.Text;
            Response.Redirect("Articulos.aspx");
        }

    }
}