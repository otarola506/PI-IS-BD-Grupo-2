using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

namespace Iteracion_2.Pages
{
    public class vistaMeritosModel : PageModel
    {
        public controladorMeritos controMeritos { set; get; }

        public string pruebaAutores { get; set; }

        public string Merito { set; get; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            //aqui solo pueden ir los cambioa a las vistas.
            controMeritos = new controladorMeritos();
            Merito = controMeritos.revisarEstadoArticulo(1); // Cambiar este valor quemado

            controMeritos.asignarPuntajeInicial(2);



        }
    }
}