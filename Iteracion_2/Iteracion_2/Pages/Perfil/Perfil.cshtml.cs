using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

namespace Iteracion_2.Pages
{
    public class PerfilModel : PageModel
    {
        private PerfilController perfilController { set; get; }

        public string[] informacionPersonal { get; private set; }
        public List<List<string>> articulosUsuario { get; private set; }
        public IActionResult OnGet()
        {
            perfilController = new PerfilController();

            informacionPersonal = perfilController.RetornarDatosPerfil("otarola506");

            articulosUsuario = perfilController.RetornarArticulosMiembro("otarola506");

            return Page();
        }
    }
}