using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Iteracion_2.Pages.Cuenta
{
    public class IngresarModel : PageModel
    {

        private MiembroController miembroController { set; get; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            miembroController = new MiembroController();
            string nombreUsuario = Request.Form["nombreUsuario"];
            string contrasenia = Request.Form["contrasenia"];

            if (nombreUsuario.Equals(""))
            {
                ViewData["username"] = "No digito un nombre usuario";
                return Page();
            }
            //if (contrasenia.Equals(""))
            //{
            //    ViewData["username"] = "No digito su contraseña";
            //    return Page();
            //}

            if (!miembroController.IngresarCuenta(nombreUsuario))
            {
                ViewData["username"] = "Usuario incorrecto.";
                return Page();
            }else
            {
                HttpContext.Session.SetString("UsuarioActual", nombreUsuario);

                return RedirectToPage("/Perfil/Perfil");
            }

        }

        public void OnPostCrear()
        {
            Response.Redirect("Registrar");
        }
    }
}