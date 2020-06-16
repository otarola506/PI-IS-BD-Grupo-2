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
            string conString = @"Server=172.16.202.75;Database=BD_Grupo2;persist security info=True;MultipleActiveResultSets=True;User ID=Grupo2;Password=grupo2.";
            con = new SqlConnection(conString);
            con.Open();
            return con;
        }
    }
}
