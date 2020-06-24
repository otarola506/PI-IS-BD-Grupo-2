using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using HtmlAgilityPack;
namespace Iteracion_2.Pages.Articulos
{
    public class RevisionModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyPeso = "PesoActual";
        const string SessionKeyTipo = "TipoActual";
        private ArticuloController ArticuloController { get; set; }
        private MiembroController MiembroController { get; set; }
        private EmailController EmailController { get; set; }

        

        public List<List<string>> ArticulosPendientes { get; set; }

        public List<List<string>> ArticulosSolicitados { get; set; }

        public string UsuarioActual { get; set; }
        public string Message;

        public string Titulo { get; set; }
        public string TipoUsuarioActual { get; set; }

        public List<List<String>> ResultadoSolicitud { get; set; }

        public IActionResult OnGet(string envio, int articuloId)
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string PesoActual = HttpContext.Session.GetString(SessionKeyPeso);
            TipoUsuarioActual = HttpContext.Session.GetString(SessionKeyTipo);

            ArticuloController = new ArticuloController();

            if (envio == "ajax")
            {
                RetornarResultadoSolicitud(articuloId);
                return Page();
            }
            else
            {
                if (UsuarioActual != null && PesoActual == "5" && TipoUsuarioActual == "coordinador")
                {
                    ArticulosPendientes = ArticuloController.RetornarPendientes();
                    object temp;
                    TempData.TryGetValue("resultadoSolicitud", out temp);

                    if (temp != null)
                    {
                        Message = (string)temp;
                    }
                    return Page();
                }
                else if (UsuarioActual != null && PesoActual == "5")
                {
                    ArticulosSolicitados = ArticuloController.RetornarArticulosPendientes(UsuarioActual, "solicitado");
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Cuenta/Ingresar", new { Mensaje = "Permisos insuficientes" });
                }
            }
        }

        public async Task <IActionResult> OnPost(string value) { 
            int id = Int32.Parse(Request.Form["artID"]);
            string titulo = Request.Form["titulo"];
            ArticuloController = new ArticuloController();
            EmailController = new EmailController();
            ArticuloController.MarcarArtSolicitado(id);
            TempData["resultadoSolicitud"] = "La solicitud ha sido enviada exitosamente a los miembro de núcleo";
            await EmailController.CorreoANucleo(titulo,"solicitar",null);


            return RedirectToPage("/Articulos/Revision");
        }

        public async Task<IActionResult> OnPostAsignar()
        {
            List<String> revisores = new List<String> { "Coordinador", "otarola506", "Dasc12" };
            int articuloId = Int32.Parse(Request.Form["artIdRevisar"]);
            string titulo = Request.Form["tituloRevisar"];

            EmailController = new EmailController();
            ArticuloController = new ArticuloController();

            ArticuloController.AsignarArticulo(articuloId, revisores);
            await EmailController.CorreoANucleo(titulo, "asignar", revisores);

            return RedirectToPage("/Articulos/Revision");
        }

        private void RetornarResultadoSolicitud(int articuloId) {
            ArticuloController = new ArticuloController();

            ResultadoSolicitud = ArticuloController.RetornarResultadoSolicitud(articuloId);
        }
    }
}