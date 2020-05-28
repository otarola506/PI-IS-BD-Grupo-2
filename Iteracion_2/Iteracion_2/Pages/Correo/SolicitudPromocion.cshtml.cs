using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

namespace Iteracion_2.Pages.Correo
{
    public class SolicitudPromocionModel : PageModel
    {
        public EmailController controlador { get; set; }
        public async Task<IActionResult> OnPost()
        {
            controlador = new EmailController();
            string destinatarioCorreo = Request.Form["destinatario"];

            string asunto = Request.Form["asunto"];
            string contenido = Request.Form["contenido"];
            //await controlador.enviarCorreo(destinatarioCorreo, asunto, contenido, archivo);

            TempData["resultado"] = "El correo ha sido enviado a " + destinatarioCorreo;
            return new RedirectToPageResult("Correo");
        }
        public void OnGet()
        {

        }
    }
}