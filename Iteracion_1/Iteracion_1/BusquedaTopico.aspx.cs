﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Iteracion_1
{
    public partial class BusquedaTopico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // EN Initial Catalog se pone la base de datos que quieran consultar, BD_CARNET
            // EN UserID ponen el usuario que usan para entrar a la base de datos
            // En password usan la clave que usan para entrar a la base de datos
            //Para ejecutar la vara usan CTRL + F5

            SqlConnection cn = new SqlConnection("Data Source=172.16.202.24,1433;Network Library=DBMSSOCN;Initial Catalog=BD_Grupo2;User ID=Grupo2;Password=grupo2.");
            // Ahorita el comando solo pregunta por el ID pero puede preguntar por otras varas más si lo modifican
            SqlCommand cmd = new SqlCommand("SELECT A.* FROM Articulo A JOIN Art_Categoria AC ON A.artIdPK = AC.artIdFK JOIN Categoria C ON AC.categoriaIdFK = C.categoriaIdPK WHERE  C.nombre ="+ "'"+ DropDownList1.SelectedValue + "'", cn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            tabla.DataSource = dt;
            tabla.DataBind();
            cn.Close();
            Response.Write("Datos Cargados Correctamente");
           // Session["param"] = DropDownList1.SelectedValue;
            //Response.Redirect("Display.aspx");
        }
    }
}