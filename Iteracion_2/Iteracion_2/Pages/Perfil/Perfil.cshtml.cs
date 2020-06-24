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
        private PerfilController PerfilController { set; get; }

        public List<String> InformacionPersonal { get; set; }

        public string UsuarioActual { get; set; }
        public List<List<string>> ArticulosUsuario { get; set; }
        public IActionResult OnGet(string UsuarioURL)
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            if (UsuarioActual != null || UsuarioURL != null)
            {
                PerfilController = new PerfilController();

                InformacionPersonal = PerfilController.RetornarDatosPerfil(UsuarioURL ?? UsuarioActual);

                ArticulosUsuario = PerfilController.RetornarArticulosMiembro(UsuarioURL ?? UsuarioActual);

                return Page();
            }
            return RedirectToPage("/Cuenta/Registrar");
        }

        public IActionResult OnPostSend()
        {
            string nombreUsuario = this.UsuarioActual;
            return RedirectToPage("/Correo/SolicitudPromocion", new { Username = nombreUsuario });
        }
    }
}