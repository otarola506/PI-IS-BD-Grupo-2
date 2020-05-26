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

        public bool crearCuenta(string nombreUsuario, string nombre, int peso)
        {
            miembroModel = new MiembroModel();
            if (!miembroModel.verificarNombreUsuario(nombreUsuario))
            {
                miembroModel.crearCuenta(nombreUsuario, nombre, peso);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool validarNombreUsuario(string nombreUsuario) {
            miembroModel = new MiembroModel();
            return miembroModel.verificarNombreUsuario(nombreUsuario);     
        }
    }
}
