using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;


namespace Iteracion_2.Models
{
    public class ConexionBD
    {
        private SqlConnection con;
        public void connection()
        {
            string conString = @"Server=172.16.202.75;Database=BD_B78451;persist security info=True;MultipleActiveResultSets=True;User ID=B78451;Password=contra";

            con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand prueba = new SqlCommand("SELECT * FROM TablaPrueba", con);

            //con.Close();
        }


        //Metodo que se encarga de retornar un string con el estado del articulo
        //Parametro: El artID del articulo el cual se quiere revisar
        public string revisarEstadoArticulo(int artID)
        {
            con.Open();
            string valor = "";
            SqlCommand cmd = new SqlCommand("RecuperarEstadoArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@artID", SqlDbType.VarChar).Value = artID; // le pasamos el artID
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                valor = reader[0].ToString();
            }
            reader.Close();

            con.Close();
            return valor;
        }



    }
}
