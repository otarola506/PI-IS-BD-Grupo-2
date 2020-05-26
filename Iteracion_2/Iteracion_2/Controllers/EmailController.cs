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
        
        // Esta logica se tiene que ir de aqui para definirla en el modelo, acá crear un método que solo pase los datos de la vista al modelo
        public async Task enviarCorreo(string destinatario, string asunto, string contenido,IFormFile archivo) {
            sendMail = new Email();
            await sendMail.envioCorreo(destinatario,asunto,contenido,archivo);
            

        }
    }
}
