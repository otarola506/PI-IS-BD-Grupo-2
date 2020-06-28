using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasComunidadPractica.Merito
{
    public interface IMeritoModel
    {
        int ObtenerPeso(string NombreUsuario);
        string DegradarPeso(string NombreUsuario);
    }
}
