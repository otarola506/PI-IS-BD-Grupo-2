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
        //Aca se llaman a los name de los form para pasarselos al controlador por medio de parametros
        //En el caso de recuperar los correos necesito hacer un on get que llame al método del controlador
        [BindProperty]
        public EmailController controlador { get; set; }
        [BindProperty]
        public Email sendMail { get; set; } 
        [BindProperty]
        public Perfil perf { get; set; }

        
        public List<string> mostrarDatos;
       
        public  async Task OnPost() {
           await controlador.enviarCorreo(sendMail);
            
           ViewData["Message"] = "El correo ha sido enviado a " + sendMail.To;
        }



    }
}