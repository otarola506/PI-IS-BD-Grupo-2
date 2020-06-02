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

        private MiembroController MiembroController { set; get; }

        public void OnGet(string Mensaje)
        {
            if (Mensaje != null) {
                ViewData["alerta"] = Mensaje;
            }
        }

        public IActionResult OnPost()
        {
            MiembroController = new MiembroController();
            string nombreUsuario = Request.Form["nombreUsuario"];
            string contrasenia = Request.Form["contrasenia"];

            if (nombreUsuario.Equals(""))
            {
                ViewData["alerta"] = "No digito un nombre usuario";
                return Page();
            }

            if (!MiembroController.IngresarCuenta(nombreUsuario))
            {
                ViewData["alerta"] = "Usuario incorrecto.";
                return Page();
            }else
            {
                HttpContext.Session.SetString("UsuarioActual", nombreUsuario);

                string pesoMiembro = MiembroController.RetornarPesoMiembro(nombreUsuario);
                HttpContext.Session.SetString("PesoActual", pesoMiembro);

                return RedirectToPage("/Perfil/Perfil");
            }
        }

        public void OnPostCrear()
        {
            Response.Redirect("Registrar");
        }
    }
}