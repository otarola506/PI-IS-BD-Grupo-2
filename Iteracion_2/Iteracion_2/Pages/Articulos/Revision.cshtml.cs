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
        private ArticuloController ArticuloController { get; set; }
        

        public List<List<string>> ArticulosPendientes { get; set; }

        public string UsuarioActual { get; set; }

        public int ArtIDPk { get; set;}

        public string Titulo { get; set; }
        public IActionResult OnGet()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string PesoActual = HttpContext.Session.GetString(SessionKeyPeso);

            ArticuloController = new ArticuloController();

            if (UsuarioActual != null && PesoActual == "5")
            {
                ArticulosPendientes = ArticuloController.RetornarPendientes();
                return Page();
            }
            else {
                return RedirectToPage("/Cuenta/Ingresar", new {Mensaje = "Permisos insuficientes" });
            }
        }

        public IActionResult OnPost()
        {

            int ArtId = ArtIDPk;
            string TituloArt = Titulo;
            return Page();
        }
        // Método para que solo aparezca el botón para el coordinador.
        //Metódo onPost que pasa datos de artículo al controlador para que este se los pase al modelo de correo.


    }
}