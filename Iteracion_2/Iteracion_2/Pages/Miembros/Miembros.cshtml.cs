using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages.Miembros
{
    public class MiembrosModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyPeso = "PesoActual";
        private MiembroController MiembroController { get; set; }

        public List<List<string>> MiembrosComunidad { get; set; }

        private MeritoController ControladorMerito { get; set; }

        public string PesoMiembroActual { get; set; }


        

        [BindProperty]
        public string NombreUsuario { get; set; }

        public string Mensaje { get; set; }

        public IActionResult OnGet(string Retroalimentacion)
        {
            MiembroController = new MiembroController();
            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            PesoMiembroActual = HttpContext.Session.GetString(SessionKeyPeso);

            MiembrosComunidad = MiembroController.RetornarMiembros();

            if (Retroalimentacion != null)
            {
                Mensaje = Retroalimentacion;
            }

            return Page();
        }

        public IActionResult OnPostModificar()
        {
            ControladorMerito = new MeritoController();
            string Respuesta = "";

            Respuesta = ControladorMerito.DegradarMiembros(NombreUsuario);

            return RedirectToPage("/Miembros/Miembros", new { Retroalimentacion = Respuesta});
        }

    }
}