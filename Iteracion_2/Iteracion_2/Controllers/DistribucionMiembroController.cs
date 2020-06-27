using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class DistribucionMiembroController
    {
        DistribucionMiembroModel ModeloDistribucion { get; set; }
        

        public List<ReporteConfigurable> ComunicarDatosDistrubucion(string [] Valores )
        {
            ModeloDistribucion = new DistribucionMiembroModel();
            
            return ModeloDistribucion.EncontrarValoresDistribucion(Valores);
        }


        public string ComunicarSeleccion(string Seleccion)
        {
            ModeloDistribucion = new DistribucionMiembroModel();

            return ModeloDistribucion.OptenerValoresSeleccion(Seleccion);
        }


    }
}
