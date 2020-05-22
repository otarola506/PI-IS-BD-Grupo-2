using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace Iteracion_2.Models
{
    public class Email
    {
        private SqlConnection con;
        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["conexionECCI"].ToString();
            con = new SqlConnection(conString);
        }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        // Esto se hace en el model perfil o miembro para recuperar correos por el momento esto es solo para pruebas
        public IEnumerable<string> recuperarCorreos() {
            SqlCommand cmd = new SqlCommand("Recuperar_Correos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List <string> results = new List<string>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    results.Add(reader["correo"].ToString());
            }
            return results;

        }
    }
}
