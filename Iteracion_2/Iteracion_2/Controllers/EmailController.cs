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
        public EmailModel sendMail { get; set; }
        public PerfilModel perf { get; set; }

        public MiembroModel Miembros { get; set; }

        public async Task enviarCorreo(string destinatario, string asunto, string contenido,IFormFile archivo) {
            sendMail = new EmailModel();
            await sendMail.enviarCorreo(destinatario,asunto,contenido,archivo);
            

        }

        public bool verificarCorreo(string Usuario)
        {
            perf = new PerfilModel();
            return perf.verificarCorreo(Usuario);

        }

        public async Task enviarSolicitud(string contenido, string Usuario)
        {
            sendMail = new EmailModel();
            await sendMail.enviarSolicitud(contenido, Usuario);
        }

        public List<string> recuperarCorreos() {
            Miembros= new MiembroModel();
            return Miembros.RecuperarCorreosMiembros();
        }

        public string obtenerCorreo(string Usuario) {
            perf = new PerfilModel();
            return perf.obtenerCorreo(Usuario);
        }
    }
}
