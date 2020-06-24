using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Iteracion_2.Controllers;

namespace Iteracion_2.Pages.Analitica
{
    public class DistribucionMiembrosModel : PageModel
    {

        public int NumeroConsulta { get; set; }
        [BindProperty]
        public string Valores { get; set; }

        public List< List<string> > ValoresGrafica { set; get; }

        DistribucionMiembroController ControladorDistribucion { get; set; }

        public string[][] MatrizDatos { get; set; }



        public void OnGet()
        {
            NumeroConsulta = 0;

        }


        public IActionResult OnPost()
        {
            
            string[] Selecciones = new string[2];
            Selecciones = Valores.Split(',');
            ControladorDistribucion = new DistribucionMiembroController();
            //List<List<string>> ValoresGrafico = new List<List<string>>();
            ValoresGrafica = ControladorDistribucion.ComunicarDatosDistrubucion(Selecciones);
            MatrizDatos = ValoresGrafica.Select(a => a.ToArray()).ToArray();

            

            NumeroConsulta++;
            return Page();
        }
    }
}