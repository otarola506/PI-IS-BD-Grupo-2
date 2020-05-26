using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

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
            

            if (!miembroController.crearCuenta(nuevo_nombreUsuario, nuevo_nombre, peso)) {
                ViewData["username"] = "Este nombre usuario ya existe";
                return Page();
            }
            else
            {
                miembroController.crearPerfil(nuevo_nombreUsuario, "edite", 0);
                return RedirectToPage("/Perfil/Perfil",new { Usuario = nuevo_nombreUsuario});
            }
        }

        public IActionResult OnGet() {
            ViewData["username"] = "Registrese para ver perfil";
            return Page();
        }
      
    }
}