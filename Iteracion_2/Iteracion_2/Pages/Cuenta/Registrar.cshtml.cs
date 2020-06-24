using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Pages
{
    public class RegistrarModel : PageModel
    {
        private MiembroController miembroController { set; get; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            miembroController = new MiembroController();
            string nuevo_nombreUsuario = Request.Form["nombreUsuario"].ToString();
            string nuevo_nombre = Request.Form["Nombre"].ToString();
            string nuevo_apellido = Request.Form["Apellido"].ToString();
            string nuevo_correo = Request.Form["Correo"].ToString();
            string nuevo_pais = Request.Form["Pais"].ToString();
            string nuevo_idioma = Request.Form["Idioma"].ToString();
            string nuevo_hobbies = Request.Form["Hobbies"].ToString();
            string nuevo_habilidades = Request.Form["Habilidades"].ToString();
         

            if (nuevo_nombreUsuario.Equals("")) {
                ViewData["username"] = "Hacen falta datos";
                return Page();
            }

            if (nuevo_nombre.Equals("")) {
                ViewData["username"] = "Hacen falta datos";
                return Page();
            }

            if (nuevo_apellido.Equals(""))
            {
                ViewData["username"] = "Hacen falta datos";
                return Page();
            }

            if (nuevo_correo.Equals(""))
            {
                ViewData["username"] = "Hacen falta datos";
                return Page();
            }

            string[] informacionNueva = new string[]
                             {
                                            nuevo_nombreUsuario,
                                            nuevo_nombre,
                                            nuevo_apellido,
                                            nuevo_correo,
                                            nuevo_pais,
                                            nuevo_idioma,
                                            nuevo_hobbies,
                                            nuevo_habilidades
                             };

            if (!miembroController.CrearCuenta(informacionNueva)) {
                ViewData["username"] = "Este nombre usuario ya existe";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("UsuarioActual", nuevo_nombreUsuario);
                HttpContext.Session.SetString("PesoActual", "0");
                HttpContext.Session.SetString("TipoActual", "periferico");

    

                return RedirectToPage("/Perfil/EditarPerfil");
            }
        }

        public IActionResult OnGet() {
            ViewData["username"] = "Registrese para ver perfil";
            return Page();
        }

        public void OnPostIngresar()
        {
            Response.Redirect("Ingresar");
        }

    }
}