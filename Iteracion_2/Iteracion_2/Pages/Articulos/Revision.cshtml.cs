﻿using System;
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
               
        public string UsuarioActual { get; set; }
        public string Message;

        public string Titulo { get; set; }
        public string TipoUsuarioActual { get; set; }

        

        public IActionResult OnGet()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            string PesoActual = HttpContext.Session.GetString(SessionKeyPeso);
            TipoUsuarioActual = HttpContext.Session.GetString(SessionKeyTipo);

            ArticuloController = new ArticuloController();

            
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
                ArticulosPendientes = ArticuloController.RetornarArticulosPendientes(UsuarioActual, "solicitado");
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

        public async Task <IActionResult> OnPost() { 
            int id = Int32.Parse(Request.Form["artID"]);
            string titulo = Request.Form["titulo"];
            ArticuloController = new ArticuloController();
            EmailController = new EmailController();
            ArticuloController.MarcarArtSolicitado(id);
            TempData["resultadoSolicitud"] = "La solicitud ha sido enviada exitosamente a los miembros de núcleo";
            await EmailController.CorreoANucleo(titulo,"solicitar",null);


            return RedirectToPage("/Articulos/Revision");
        }

        public async Task<IActionResult> OnPostAceptarRechazar()
        {
            UsuarioActual = HttpContext.Session.GetString(SessionKeyUsuario);
            int id = Int32.Parse(Request.Form["artID"]);
            string titulo = Request.Form["titulo"];
            string estado = Request.Form["estado"];
            ArticuloController = new ArticuloController();
            EmailController = new EmailController();
            TempData["resultadoSolicitud"] = "La respuesta ha sido enviada al coordinador exitosamente";
            if (estado == "aceptado")
            {
                ArticuloController.ModificarEstadoSolicitud(id, UsuarioActual, "aceptado");
                await EmailController.CorreoACoordinadores(titulo, "aceptado", UsuarioActual);
            }
            else
            {
                ArticuloController.ModificarEstadoSolicitud(id, UsuarioActual, "rechazado");
                await EmailController.CorreoACoordinadores(titulo, "rechazado", UsuarioActual);
            }
              
            return RedirectToPage("/Articulos/Revision");
        }

    }
}