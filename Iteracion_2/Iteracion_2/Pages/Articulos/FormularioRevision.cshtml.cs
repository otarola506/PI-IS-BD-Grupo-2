using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;
using System.Windows;


namespace Iteracion_2.Pages.Articulos
{
    public class FormularioRevisionModel : PageModel
    {
        FormularioRevisionController FormularioContro { get; set; }

        ArticuloController ArticuloContro { get; set; }

        public string[] informacionArticulo { get; private set; }

        public string autores { get; set; }
        [TempData]
        public string Message { get; set; }

        public List<string> autor { get; private set; }

        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyPesoUsuario = "PesoActual";

        public string ArticuloID { get; private set; }

        public IActionResult OnGet(string artId)
        {
            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string PesoActual = HttpContext.Session.GetString(SessionKeyPesoUsuario);

            if(PesoActual == "5" && artId != null)
            {
                ArticuloContro = new ArticuloController();
                autores = "";
                informacionArticulo = ArticuloContro.RetornarDatos(artId);
                autor = ArticuloContro.RetornarAutor(artId);
                for (int index = 0; index < autor.Count; index++)
                {
                    if (index != 0)
                    {
                        autores += " , ";
                    }
                    autores += autor[index] + " ";
                }


                ArticuloID = artId;


                return Page();
            }
            return RedirectToPage("/Cuenta/Ingresar", new { Mensaje = "Permisos insuficientes" });

        }

        public IActionResult OnPost(string artId)
        {

            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string opinion = Request.Form["Opinion"].ToString();
            string contribucion = Request.Form["Contribucion"].ToString();
            string forma = Request.Form["Forma"].ToString();
            string observaciones = "" + Request.Form["comentarios"].ToString();

            if (opinion.Equals("") || contribucion.Equals("") || forma.Equals(""))
            {

                Message = "No ha seleccionado todas las calificaciones";
                return RedirectToPage("FormularioRevision", new { artId = artId });
            }


            int opinionInt = Int16.Parse(opinion);
            int contribucionInt = Int16.Parse(contribucion);
            int formaInt = Int16.Parse(forma);



            ArticuloID = artId;



            FormularioContro = new FormularioRevisionController();
            bool validado = FormularioContro.ProcesarFormulario(opinionInt, contribucionInt, formaInt, observaciones, UsuarioActual, ArticuloID);
            if (validado)
            {
                return RedirectToPage("/Articulos/Revision");
            }
            else {
                Message = "Intentar de nuevo.";
                return RedirectToPage("FormularioRevision", new { artId = artId });

            }

            

        }
    }
}