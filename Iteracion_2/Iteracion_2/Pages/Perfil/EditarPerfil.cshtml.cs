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

        public IActionResult OnGet()
        {
            perfilController = new PerfilController();

            informacionPersonal = perfilController.RetornarDatosPerfil("otarola506");

            object temp;
            TempData.TryGetValue("resultado", out temp);

            if (temp != null) {
                Message = (string)temp;
            }
            
            return Page();
        }

        public IActionResult OnPostSave()
        {
            perfilController = new PerfilController();

            string informacionLaboral= Request.Form["laboral"].ToString();
            string informacionBiografica = Request.Form["biografica"].ToString();
            string telefono = Request.Form["telefono"].ToString();
            string correo = Request.Form["correo"].ToString();

            string[] informacionActualizada = new string[] { informacionLaboral, informacionBiografica, telefono, correo };

            perfilController.GuardarDatosPerfil("otarola506", informacionActualizada);

            TempData["resultado"] = "Información actualizada exitosamente";

            return new RedirectToPageResult("EditarPerfil");

        }
    }
}