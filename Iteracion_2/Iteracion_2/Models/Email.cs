using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IFormFile archivo { get; set; }
        
    }
}
