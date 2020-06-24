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
    public class RegistrarModel : PageModel
    {
        private MiembroController miembroController { set; get; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            miembroController = new MiembroController();
            string nuevo_nombreUsuario = Request.Form["inputUsername"];
            string nuevo_nombre = Request.Form["inputName"];
            int peso = 0;

            if (nuevo_nombreUsuario.Equals("")) {
                ViewData["username"] = "No digito un nombre usuario";
                return Page();
            }

            if (nuevo_nombre.Equals("")) {
                ViewData["username"] = "No digito su nombre";
                return Page();
            }

            if (!miembroController.crearCuenta(nuevo_nombreUsuario, nuevo_nombre, peso)) {
                ViewData["username"] = "Este nombre usuario ya existe";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("UsuarioActual", nuevo_nombreUsuario);
                HttpContext.Session.SetString("PesoActual", "0");
                HttpContext.Session.SetString("TipoActual", "periferico");

                miembroController.crearPerfil(nuevo_nombreUsuario, "", 0);

                return RedirectToPage("/Perfil/EditarPerfil");
            }
        }

        public IActionResult OnGet() {
            ViewData["username"] = "Registrese para ver perfil";
            return Page();
        }

        public void OnPostIngresar()
        {
            Response.Redirect("Ingresar");
        }

    }
}