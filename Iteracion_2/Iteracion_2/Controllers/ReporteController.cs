using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class ReporteController
    {
        ReporteModel ModeloDistribucion { get; set; }
        

        public List<ReporteConfigurable> ComunicarDatosDistrubucion(string [] Valores )
        {
            ModeloDistribucion = new ReporteModel();
            
            return ModeloDistribucion.EncontrarValoresDistribucion(Valores);
        }


        public string ComunicarSeleccion(string Seleccion)
        {
            ModeloDistribucion = new ReporteModel();

            return ModeloDistribucion.OptenerValoresSeleccion(Seleccion);
        }


    }
}
