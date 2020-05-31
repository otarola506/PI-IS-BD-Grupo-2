using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages.Perfil
{
    public class EditarPerfilModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        private PerfilController perfilController { set; get; }

        public string Message { get; set; }
        public string[] InformacionPersonal { get; private set; }

        public string UsuarioActual { get; set; }

        public IActionResult OnGet()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            if (UsuarioActual != null) {
                perfilController = new PerfilController();

                InformacionPersonal = perfilController.RetornarDatosPerfil(UsuarioActual);

                TempData.TryGetValue("resultado", out object temp);

                if (temp != null)
                {
                    Message = (string)temp;
                }

                return Page();
            }
            return RedirectToPage("/Registrar/Registrar");

        }

        public IActionResult OnPostSave()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            perfilController = new PerfilController();

            string nombre = Request.Form["nombre-completo"].ToString();
            string informacionLaboral= Request.Form["laboral"].ToString();
            string informacionBiografica = Request.Form["biografica"].ToString();
            string telefono = Request.Form["telefono"].ToString();
            string correo = Request.Form["correo"].ToString();

            string[] informacionActualizada = new string[] { nombre, informacionLaboral, informacionBiografica, telefono, correo };

            perfilController.GuardarDatosPerfil(UsuarioActual, informacionActualizada);

            TempData["resultado"] = "Información actualizada exitosamente";

            return RedirectToPage("/Perfil/EditarPerfil");
        }
    }
}