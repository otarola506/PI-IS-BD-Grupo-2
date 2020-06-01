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
        private MiembroController MiembroController { get; set; }

        public List<List<string>> MiembrosComunidad { get; set; }

        private MeritoController ControladorMerito { get; set; }

        public string PesoMiembroActual { get; set; }


        const string SessionKeyUsuario = "UsuarioActual";

        [BindProperty]
        public string NombreUsuario { get; set; }

        public string Mensaje { get; set; }

        public IActionResult OnGet(string Retroalimentacion)
        {
            MiembroController = new MiembroController();
            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            //if (UsuarioActual == null)
            //{
            //    UsuarioActual = "";
            //}
            var valoresMiembro = MiembroController.RetornarMiembros(UsuarioActual);
            MiembrosComunidad = valoresMiembro.Item1;
            PesoMiembroActual = valoresMiembro.Item2;

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