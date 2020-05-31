using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Registrar
{
    public class DesconectarseModel : PageModel
    {
        public void OnGet()
        {
            ViewData.Remove("usuarioActual");
            HttpContext.Session.Remove("UsuarioActual");
            Response.Redirect("/Index");
        }
    }
}