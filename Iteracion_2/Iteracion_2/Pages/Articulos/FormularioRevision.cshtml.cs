using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages.Articulos
{
    public class FormularioRevisionModel : PageModel
    {
        FormularioRevisionController FormularioContro { get; set; }

        public string[] informacionArticulo { get; private set; }

        public string [] autor { get; private set; }

        const string SessionKeyUsuario = "UsuarioActual";
         string SessionKeyPesoUsuario = "PesoActual";



        public void OnGet()
        {

        }




        public void OnPost()
        {

            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string opinion = Request.Form["Opinion"].ToString();
            string contribucion = Request.Form["Contribucion"].ToString();
            string forma = Request.Form["Forma"].ToString();
            string observaciones = "" + Request.Form["comentarios"].ToString();

            int opinionInt = Int16.Parse(opinion);
            int contribucionInt = Int16.Parse(contribucion);
            int formaInt = Int16.Parse(forma);


            //Atributo de preuba 

            string artID = "1";


            FormularioContro = new FormularioRevisionController();
            FormularioContro.ProcesarFormulario(opinionInt, contribucionInt, formaInt, observaciones, UsuarioActual, artID);

            //Redireccion
            


        }
    }
}