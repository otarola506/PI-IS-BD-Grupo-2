using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages.Correo
{
    public class SolicitudPromocionModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        public EmailController controlador { get; set; }

        public string UsuarioActual { get; set; }

        public IActionResult OnGet(string Username)
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            controlador = new EmailController();
            this.UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string contenido = Request.Form["contenido"];
            //If si tiene correo 
            if (controlador.verificarCorreo(UsuarioActual))
            {
                await controlador.enviarSolicitud(contenido, UsuarioActual);
                ViewData["username"] = "Su correo ha sido enviado satisfactoriamente";
                return Page();
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