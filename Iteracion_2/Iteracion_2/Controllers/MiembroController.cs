using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class MiembroController
    {
        private MiembroModel miembroModel { set; get; }

        public void crearCuenta(string nombreUsuario, string nombre, int peso)
        {
            miembroModel = new MiembroModel();
            miembroModel.crearCuenta(nombreUsuario, nombre, peso);
        }
    }
}
