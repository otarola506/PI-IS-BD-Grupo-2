using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Articulos
{
    public class MisArticulosModel : PageModel
    {
        private const string Location = "http://localhost:51359/MisArticulos?value1=";
        const string SessionKeyUsuario = "UsuarioActual";

        public void OnGet()
        {
            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            if (UsuarioActual != null) {
                Response.Redirect(Location + UsuarioActual);
            }
            //RedirectToPage("/Cuenta/Registrar");

        }
    }
}