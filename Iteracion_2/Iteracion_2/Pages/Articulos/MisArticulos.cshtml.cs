using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Articulos
{
    public class MisArchivosModel : PageModel
    {
        private const string Location = "http://localhost:51359/MisArticulos";
        Encoding unicode = Encoding.Unicode;
        public void OnGet()
        {
            Response.Redirect(Location);
            
        }
    }
}