using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Preguntas
{
    public class AgregarPreguntaFrecuenteModel : PageModel
    {
        private const string Location = "http://localhost:51359/AgregarPregFrecMP";
        public void OnGet()
        {
            Response.Redirect(Location);
        }
    }
}