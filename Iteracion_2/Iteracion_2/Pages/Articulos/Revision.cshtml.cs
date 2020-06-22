using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages.Articulos
{
    public class RevisionModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyPeso = "PesoActual";
        const string SessionKeyTipo = "TipoActual";
        private ArticuloController ArticuloController { get; set; }
        private EmailController EmailController { get; set; }
        

        public List<List<string>> ArticulosPendientes { get; set; }

        public string UsuarioActual { get; set; }
        public int ArtId { get; set; }

        public string Titulo { get; set; } 

        public IActionResult OnGet()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string PesoActual = HttpContext.Session.GetString(SessionKeyPeso);
            string tipo = HttpContext.Session.GetString(SessionKeyTipo);

            ArticuloController = new ArticuloController();

            if (UsuarioActual != null && PesoActual == "5" && tipo == "coordinador")
            {
                ArticulosPendientes = ArticuloController.RetornarPendientes();
                return Page();
            }
            else {
                return RedirectToPage("/Cuenta/Ingresar", new {Mensaje = "Permisos insuficientes" });
            }
        }

        public async Task <IActionResult> OnPost() { 
            int id = Int32.Parse(Request.Form["artID"]);
            string titulo = Request.Form["titulo"];
            ArticuloController = new ArticuloController();
            EmailController = new EmailController();
            ArticuloController.MarcarArtSolicitado(id);
            await EmailController.EnviarSolicitudNucleo(titulo);


            return RedirectToPage("/Articulos/Revision");
        }

    }
}