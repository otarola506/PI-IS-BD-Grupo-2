using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Iteracion_2.Pages.Articulos
{
    public class AsignarRevisorModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyTipo = "TipoActual";

        private ArticuloController ArticuloController { get; set; }
        private EmailController EmailController { get; set; }

        public List<List<String>> ResultadoSolicitud { get; set; }

        public IActionResult OnGet(int articuloId)
        {
            TempData["articuloId"] = articuloId;

            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string TipoUsuarioActual = HttpContext.Session.GetString(SessionKeyTipo);
            if (articuloId != 0 && TipoUsuarioActual == "coordinador") {

                ArticuloController = new ArticuloController();

                ResultadoSolicitud = ArticuloController.RetornarResultadoSolicitud(articuloId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsignar()
        {
            string articuloId = TempData["articuloId"].ToString();
               
            string getListaRevisores = Request.Form["listaRevisores"];
            string[] revisores = getListaRevisores.Split(',');
            

            EmailController = new EmailController();
            ArticuloController = new ArticuloController();

            ArticuloController.AsignarArticulo(Int16.Parse(articuloId), revisores);
            await EmailController.CorreoANucleo("no lo se guardarjejes", "asignar", revisores);
            return RedirectToPage("/Articulos/Revision");
        }
    }
}