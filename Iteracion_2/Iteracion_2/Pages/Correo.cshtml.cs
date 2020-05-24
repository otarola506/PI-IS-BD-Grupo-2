using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Controllers;
using Iteracion_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages
{
    public class CorreoModel : PageModel
    {
        [BindProperty]
        public EmailController controlador { get; set; }
        [BindProperty]
        public Email sendMail { get; set; } 

        public  async Task OnPost() {
           await controlador.enviarCorreo(sendMail);
            
           ViewData["Message"] = "El correo ha sido enviado a " + sendMail.To;
        }

    }
}