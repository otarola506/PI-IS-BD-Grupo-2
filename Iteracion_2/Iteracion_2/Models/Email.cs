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
        // Definir un método recuperar correos que haga el envío de correos con los atributos que tengo.
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile archivo { get; set; }
        
    }
}
