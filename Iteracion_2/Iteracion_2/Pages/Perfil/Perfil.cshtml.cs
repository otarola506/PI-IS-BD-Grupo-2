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

        [TempData]
        public string UsuarioActual { get; set; }
        public List<List<string>> articulosUsuario { get; private set; }
        public IActionResult OnGet(string Usuario)
        {
            if (this.UsuarioActual != null || Usuario != null) {
                this.UsuarioActual = Usuario;
                perfilController = new PerfilController();

                informacionPersonal = perfilController.RetornarDatosPerfil((this.UsuarioActual != null) ? this.UsuarioActual : Usuario);

                articulosUsuario = perfilController.RetornarArticulosMiembro((this.UsuarioActual != null) ? this.UsuarioActual : Usuario);

                return Page();
            }
            return RedirectToPage("/Registrar/Registrar");
        }
    }
}