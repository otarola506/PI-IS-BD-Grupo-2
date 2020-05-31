using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Cuenta
{
    public class DesconectarModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Remove("UsuarioActual");
            Response.Redirect("/Index");
        }
    }
}