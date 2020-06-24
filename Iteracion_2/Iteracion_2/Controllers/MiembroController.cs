using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class MiembroController
    {
        private MiembroModel MiembroModel { set; get; }

        public bool CrearCuenta(string [] info)
        {
            MiembroModel = new MiembroModel();
            if (!MiembroModel.VerificarNombreUsuario(info[0]))
            {
                MiembroModel.CrearCuenta(info);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidarNombreUsuario(string nombreUsuario) {
            MiembroModel = new MiembroModel();
            return MiembroModel.verificarNombreUsuario(nombreUsuario);     
        }


        public List<List<string>>RetornarMiembros() {
            MiembroModel = new MiembroModel();
  
            return MiembroModel.RetornarMiembros();
        }

        public bool IngresarCuenta(string NombreUsuario)
        {
            MiembroModel = new MiembroModel();
            return MiembroModel.IngresarCuenta(NombreUsuario);
        }

        public (string,string) RetornarPesoMiembroTipo(string NombreUsuario) {
            MiembroModel = new MiembroModel();
            return MiembroModel.RetornarPesoMiembroTipo(NombreUsuario);
        }

    }
}
