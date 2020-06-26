using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Iteracion_2.Pages.Articulos
{
    public class AsignarRevisorModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyTipo = "TipoActual";
        private readonly IHostingEnvironment _env;

        private ArticuloController ArticuloController { get; set; }
        private EmailController EmailController { get; set; }

        public List<List<string>> ResultadoSolicitud { get; set; }

        public AsignarRevisorModel(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult OnGet(int articuloId, string titulo)
        {
            string UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string TipoUsuarioActual = HttpContext.Session.GetString(SessionKeyTipo);
            if (articuloId != 0 && titulo != "" && TipoUsuarioActual == "coordinador")
            {
                TempData["articuloId"] = articuloId;
                TempData["titulo"] = titulo;

                ArticuloController = new ArticuloController();

                ResultadoSolicitud = ArticuloController.RetornarResultadoSolicitud(articuloId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsignar()
        {
            string articuloId = TempData["articuloId"].ToString();
            string titulo = TempData["titulo"].ToString();
               
            string temp = Request.Form["listaRevisores"];
            string getListaRevisores = temp.TrimEnd(new Char[]  { ',' });

            string[] revisores = getListaRevisores.Split(',');

            EmailController = new EmailController();
            ArticuloController = new ArticuloController();

            ArticuloController.AsignarArticulo(Int16.Parse(articuloId), revisores);
            await EmailController.CorreoANucleo(titulo, "asignar", revisores);

            TempData["resultadoSolicitud"] = "Se han asignado los revisores de '"+titulo+"' correctamente.";
            return RedirectToPage("/Articulos/Revision");
        }
    }
}