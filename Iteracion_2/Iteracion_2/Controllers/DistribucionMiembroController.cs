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
        

        public List<List<string >> ComunicarDatosDistrubucion(string [] Valores )
        {
            ModeloDistribucion = new DistribucionMiembroModel();
            
            List<List<string>> Retorno = new List<List<string>>();

            Retorno = ModeloDistribucion.EncontrarValoresDistribucion(Valores);

            return Retorno;
        }


    }
}
