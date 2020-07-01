using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;
using HtmlAgilityPack;

namespace Iteracion_2.Pages.Articulos
{
    public class RevisionFinalModel : PageModel
    {
        const string SessionKeyUsuario = "UsuarioActual";
        const string SessionKeyPeso = "PesoActual";
        const string SessionKeyTipo = "TipoActual";
        private ArticuloController ArticuloController { get; set; }
        private EmailController EmailController { get; set; }

        public List<List<string>> ArticulosRevisados { get; set; }

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
                    ArticulosRevisados = ArticuloController.RetornarRevisados();
                    object temp;
                    TempData.TryGetValue("resultadoSolicitud", out temp);

                    if (temp != null)
                    {
                        Message = (string)temp;
                    }
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Cuenta/Ingresar", new { Mensaje = "Permisos insuficientes" });
                }
            }
        }

        private void RetornarResultadoSolicitud(int articuloId)
        {
            ArticuloController = new ArticuloController();

            ResultadoSolicitud = ArticuloController.RetornarResultadoSolicitud(articuloId);
        }

        public async Task<IActionResult> OnPost()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            int id = Int32.Parse(Request.Form["artID"]);
            string titulo = Request.Form["titulo"];
            string estado = Request.Form["estado"];
            string observaciones = Request.Form["comentarios"];
            int puntuacion = Int32.Parse(Request.Form["puntuacion"]);
            string temp = Request.Form["autoresString"];
            string getListaAutores = temp.TrimEnd(new Char[] { ',' });
            string[] autores = getListaAutores.Split(',');

            ArticuloController = new ArticuloController();
            EmailController = new EmailController();

            if (estado == "aceptado")
            {
                ArticuloController.ModificarEstadoArticulo(id, "aceptado", puntuacion);
                await EmailController.CorreoAutores(titulo, "aceptado", autores);
                TempData["resultadoSolicitud"] = "El artículo " + titulo + " fue aceptado exitosamente";
            }
            else if (estado == "cambios")
            {
                ArticuloController.ModificarEstadoArticulo(id, "cambios",puntuacion);
                await EmailController.CorreoCambiosAutores(titulo, "cambios", autores, observaciones);
                TempData["resultadoSolicitud"] = "El artículo " + titulo + " fue aceptado con cambios exitosamente";
            }
            else
            {
                ArticuloController.ModificarEstadoArticulo(id, "rechazado",puntuacion);
                await EmailController.CorreoAutores(titulo, "rechazado", autores);
                TempData["resultadoSolicitud"] = "El artículo " + titulo + " fue rechazado exitosamente";
            }

            return RedirectToPage("/Articulos/RevisionFinal");
        }

    }
}