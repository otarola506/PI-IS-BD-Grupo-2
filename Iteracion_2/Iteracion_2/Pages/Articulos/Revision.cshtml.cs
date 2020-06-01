using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Articulos
{
    public class RevisionModel : PageModel
    {
        private ArticuloController ArticuloController { get; set; }

        public List<List<string>> ArticulosPendientes { get; set; }
        public void OnGet()
        {
            ArticuloController = new ArticuloController();

            ArticulosPendientes = ArticuloController.RetornarPendientes();
        }
    }
}