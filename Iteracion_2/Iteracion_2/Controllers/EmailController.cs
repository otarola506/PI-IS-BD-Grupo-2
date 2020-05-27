using Iteracion_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iteracion_2.Controllers
{
    public class EmailController
    {
        public Email sendMail { get; set; }
        public PerfilModel perf { get; set; }
        
        public async Task enviarCorreo(string destinatario, string asunto, string contenido,IFormFile archivo) {
            sendMail = new Email();
            await sendMail.enviarCorreo(destinatario,asunto,contenido,archivo);
            

        }

        public List<string> recuperarCorreos() {
            perf = new PerfilModel();
            return perf.recuperarCorreos();
        }
    }
}
