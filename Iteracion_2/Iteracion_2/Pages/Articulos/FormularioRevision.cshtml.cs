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

        ArticuloController ArticuloContro { get; set; }

        public string[] InformacionArticulo { get; private set; }

        public string Autores { get; set; }

        public List<string> Autor { get; private set; }

        const string SessionKeyUsuario = "UsuarioActual";

         public string ArticuloID { get;  private set; }

        public IActionResult OnGet(string artId)
        {
            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            ArticuloContro = new ArticuloController();
            Autores = "";
            InformacionArticulo = ArticuloContro.RetornarDatos(artId);
            Autor = ArticuloContro.RetornarAutor(artId);
            for (int index = 0; index < Autor.Count; index++)
            {
                if(index != 0)
                {
                    Autores += " , ";
                }
                Autores += Autor[index] + " " ;
            }


            ArticuloID = artId;


            return Page();
        }




        public IActionResult OnPost(string artId)
        {

            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string opinion = Request.Form["Opinion"].ToString();
            string contribucion = Request.Form["Contribucion"].ToString();
            string forma = Request.Form["Forma"].ToString();
            string observaciones = "" + Request.Form["comentarios"].ToString();

           /* if (opinion.Equals("") || contribucion.Equals("") || forma.Equals("")) {
                ViewData["Message"] = "No completo todo";
                return RedirectToPage("/Articulos/FormularioRevision");
            }*/


            int opinionInt = Int16.Parse(opinion);
            int contribucionInt = Int16.Parse(contribucion);
            int formaInt = Int16.Parse(forma);


            
            ArticuloID = artId; // ojo si se cae es por esto
            
            

            FormularioContro = new FormularioRevisionController();
            FormularioContro.ProcesarFormulario(opinionInt, contribucionInt, formaInt, observaciones, UsuarioActual, ArticuloID);

            //Redireccion

            return RedirectToPage("/Perfil/Perfil");//Hay que redireccionar hacia articulos pendientes de revision

        }
    }
}