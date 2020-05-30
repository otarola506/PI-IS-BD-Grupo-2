using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

namespace Iteracion_2.Pages
{
    public class pruebaDegradarModel : PageModel
    {
        [BindProperty]
        public string NombreUsuario { get; set; }


        MeritoController ControladorMerito { get; set; }

        public string MensajePrueba { set; get; }

        public void OnGet()
        {

        }

        public void OnPostModificar()
        {
            ControladorMerito = new MeritoController();
            ControladorMerito.DegradarMiembros(NombreUsuario);
            MensajePrueba = "LLegue hasta aqui";

        }

    }
}