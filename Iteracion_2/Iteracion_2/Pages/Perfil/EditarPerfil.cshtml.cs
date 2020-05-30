using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

namespace Iteracion_2.Pages.Perfil
{
    public class EditarPerfilModel : PageModel
    {
        private PerfilController perfilController { set; get; }

        public string Message { get; set; }
        public string[] informacionPersonal { get; private set; }

        [TempData]
        public string UsuarioActual { get; set; }

        public IActionResult OnGet(string Usuario)
        {
            if (this.UsuarioActual != null || Usuario != null) {
                perfilController = new PerfilController();

                if(Usuario != null)
                {
                    this.UsuarioActual = Usuario;
                }

                informacionPersonal = perfilController.RetornarDatosPerfil( (UsuarioActual != null)?this.UsuarioActual:Usuario);

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
            perfilController = new PerfilController();

            this.UsuarioActual = Request.Form["usuario-actual"].ToString();
            string nombre = Request.Form["nombre-completo"].ToString();
            string informacionLaboral= Request.Form["laboral"].ToString();
            string informacionBiografica = Request.Form["biografica"].ToString();
            string telefono = Request.Form["telefono"].ToString();
            string correo = Request.Form["correo"].ToString();

            string[] informacionActualizada = new string[] { nombre, informacionLaboral, informacionBiografica, telefono, correo };

            perfilController.GuardarDatosPerfil(UsuarioActual, informacionActualizada);

            TempData["resultado"] = "Información actualizada exitosamente";

            return RedirectToPage("/Perfil/EditarPerfil", new { Usuario = this.UsuarioActual });
        }
    }
}