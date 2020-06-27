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
        

        public List<ReporteConfigurable> ComunicarDatosDistrubucion(string [] valores, string tipo)
        {
            ModeloDistribucion = new ReporteModel();
            
            return ModeloDistribucion.EncontrarValoresDistribucion(valores, tipo);
        }


        public string ComunicarSeleccion(string seleccion, string tipo)
        {
            ModeloDistribucion = new ReporteModel();

            return ModeloDistribucion.OptenerValoresSeleccion(seleccion, tipo);
        }


    }
}
