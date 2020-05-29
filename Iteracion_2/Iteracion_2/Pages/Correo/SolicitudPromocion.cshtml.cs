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

        public string UsuarioNombre { get; set; }

        [TempData]
        public string UsuarioActual { get; set; }

        public IActionResult OnGet(string Username)
        {
            this.UsuarioNombre = Username;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            controlador = new EmailController();
            string contenido = Request.Form["contenido"];
            string username = Request.Form["envia"];
            //If si tiene correo 
            if (controlador.verificarCorreo(username))
            {
                await controlador.enviarSolicitud(contenido, username);
                this.UsuarioActual = username;
                ViewData["username"] = "Su correo ha sido enviado satisfactoriamente";
                return RedirectToPage("/Correo/SolicitudPromocion", new { Username = username });
            }
            else
            {
                ViewData["username"] = "No tiene un correo disponible";
                return Page();
            }
            //await controlador.enviarCorreo(destinatarioCorreo, asunto, contenido, archivo);

        }
    }
}