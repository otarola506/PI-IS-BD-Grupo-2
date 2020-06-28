using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasComunidadPractica.Merito
{
    public class MeritoModelT
    {
        private readonly IMeritoModel _meritoMiembro;

        public MeritoModelT(IMeritoModel meritoMiembro)
        {
            _meritoMiembro = meritoMiembro;
        }

        public int ObtenerPeso(string NombreUsuario)
        {
            return _meritoMiembro.ObtenerPeso(NombreUsuario);

        }

        public string DegradarPeso(string NombreUsuario)
        {
            return _meritoMiembro.DegradarPeso(NombreUsuario);
        }

    }
}
