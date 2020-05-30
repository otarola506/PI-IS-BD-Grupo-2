using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;


namespace Iteracion_2.Pages.Miembros
{
    public class MiembrosModel : PageModel
    {
        private MiembroController MiembroController { get; set; }

        public List<List<string>> MiembrosComunidad { get; set; }

        private MeritoController ControladorMerito { get; set; }


        [BindProperty]
        public string NombreUsuario { get; set; }

        public string Mensaje { get; set; }

        public IActionResult OnGet(string Retroalimentacion)
        {
            if (Retroalimentacion == null)
            {

                MiembroController = new MiembroController();

                MiembrosComunidad = MiembroController.RetornarMiembros();
            }
            else
            {

                MiembroController = new MiembroController();

                MiembrosComunidad = MiembroController.RetornarMiembros();

                Mensaje = Retroalimentacion;
            }


            return Page();
        }

        public IActionResult OnPostModificar()
        {
            ControladorMerito = new MeritoController();
            string Retroalimentacion = "";

            Retroalimentacion = ControladorMerito.DegradarMiembros(NombreUsuario);
            //Mensaje = "Su operación fue realizada con éxito.";
            return RedirectToPage("/Miembros/Miembros", new { Retroalimentacion = Retroalimentacion});
        }

    }
}