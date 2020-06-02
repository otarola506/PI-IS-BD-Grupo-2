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

        public bool crearCuenta(string nombreUsuario, string nombre, int peso)
        {
            MiembroModel = new MiembroModel();
            if (!MiembroModel.verificarNombreUsuario(nombreUsuario))
            {
                MiembroModel.crearCuenta(nombreUsuario, nombre, peso);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool validarNombreUsuario(string nombreUsuario) {
            MiembroModel = new MiembroModel();
            return MiembroModel.verificarNombreUsuario(nombreUsuario);     
        }

        public void crearPerfil(string nombreUsuario, string info, float merito)
        {
            MiembroModel = new MiembroModel();
            MiembroModel.crearPerfil(nombreUsuario,info,merito);
        }

        public (List<List<string>>,string  )RetornarMiembros( string nombreUsuario) {
            MiembroModel = new MiembroModel();
  
            return MiembroModel.RetornarMiembros(nombreUsuario);
        }



        public bool IngresarCuenta(string NombreUsuario)
        {
            MiembroModel = new MiembroModel();
            return MiembroModel.IngresarCuenta(NombreUsuario);
        }

    }
}
