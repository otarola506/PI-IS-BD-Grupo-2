﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Preguntas
{
    public class PreguntasFrecuentesModel : PageModel
    {
        private const string Location = "http://localhost:51359/SeccionPregFrecMP";
        public void OnGet()
        {
            Response.Redirect(Location);
        }
    }
}