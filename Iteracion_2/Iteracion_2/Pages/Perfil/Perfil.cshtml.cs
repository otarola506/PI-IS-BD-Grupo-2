using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages
{
    public class PerfilModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        private PerfilController perfilController { set; get; }

        public string[] informacionPersonal { get; private set; }

        public string UsuarioActual { get; set; }
        public List<List<string>> articulosUsuario { get; private set; }
        public IActionResult OnGet(string UsuarioURL)
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            if (UsuarioActual != null)
            {
                perfilController = new PerfilController();

                informacionPersonal = perfilController.RetornarDatosPerfil(UsuarioURL ?? UsuarioActual);

                articulosUsuario = perfilController.RetornarArticulosMiembro(UsuarioURL ?? UsuarioActual);

                return Page();
            }
            return RedirectToPage("/Registrar/Registrar");
        }

        public IActionResult OnPostSend()
        {
            string nombreUsuario = this.UsuarioActual;
            return RedirectToPage("/Correo/SolicitudPromocion", new { Username = nombreUsuario });
        }
    }
}