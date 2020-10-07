using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Iteracion_2.Models
{
    public class ConexionModel
    {
        
        public SqlConnection Connection()
        {
            SqlConnection con;
            string conString = @"Data Source=IVANJ;Initial Catalog=BD_Calidad;Integrated Security=True";
            con = new SqlConnection(conString);
            con.Open();
            return con;
        }
    }
}
