using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Controllers;
using Iteracion_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Correo
{
    public class CorreoModel : PageModel
    {
        //Aca se llaman a los name de los form para pasarselos al controlador por medio de parametros
        //En el caso de recuperar los correos necesito hacer un on get que llame al método del controlador
        public EmailController controlador { get; set; }
        public List<string> mostrarDatos;

        public async Task<IActionResult> OnPost()
        {
            controlador = new EmailController();
            string destinatarioCorreo = Request.Form["correoMiembro"];
            
            string asunto = Request.Form["asunto"];
            string contenido = Request.Form["contenido"];
            IFormFile archivo = Request.Form.Files["archivo"];
            await controlador.enviarCorreo(destinatarioCorreo,asunto,contenido,archivo);

            ViewData["Message"] = "El correo ha sido enviado a " + destinatarioCorreo;
            return new RedirectToPageResult("Correo");
        }

        public IActionResult OnGet() {
            controlador = new EmailController();
            mostrarDatos = controlador.recuperarCorreos();
            return Page();


        }

    }
}