using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Articulos
{
    public class FormularioRevisionModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string opinion = Request.Form["Opinion"].ToString();
            string contribucion = Request.Form["Contribucion"].ToString();
            string forma = Request.Form["Forma"].ToString();
            string observaciones = "" + Request.Form["comentarios"].ToString();

            int opinionInt = Int16.Parse(opinion);
            int contribucionInt = Int16.Parse(contribucion);
            int formaInt = Int16.Parse(forma);

        }
    }
}