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
        public IActionResult OnGet()
        {
            MiembroController = new MiembroController();

            MiembrosComunidad = MiembroController.RetornarMiembros();

            return Page();
        }
    }
}