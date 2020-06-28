using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasComunidadPractica
{
    public class MiembroModelT
    {
        public readonly IMiembroModel _miembro;
        public MiembroModelT(IMiembroModel miembro)
        {
            _miembro = miembro;

        }

        public bool VerificarNombreUsuario(string nombreUsuario)
        {
            return _miembro.VerificarNombreUsuario(nombreUsuario);



        }
    }
}
