using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Iteracion_2.Models
{
    public class Perfil
    {

        private SqlConnection con;
        public void Connection()
        {
            string conString = @"Server=172.16.202.75;Database=BD_Grupo2;persist security info=True;MultipleActiveResultSets=True;User ID=Grupo2;Password=grupo2.";
            con = new SqlConnection(conString);
        }
        public List<string> recuperarCorreos()
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Recuperar_Correos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List<string> results = new List<string>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    results.Add(reader["correo"].ToString());
            }
            con.Close();
            return results;

        }
    }

}
