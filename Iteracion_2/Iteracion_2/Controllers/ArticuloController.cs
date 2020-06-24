using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class ArticuloController : Controller
    {
        private ArticuloModel ArticuloModel { get; set; }
        public List<List<string>> RetornarPendientes() {
            ArticuloModel = new ArticuloModel();

            return ArticuloModel.RetornarPendientes();
        }

        public List<List<string>> RetornarArticulosPendientes(string nombreUsuarioActual, string estado)
        {
            ArticuloModel = new ArticuloModel();

            return ArticuloModel.RetornarArticulosPendientes(nombreUsuarioActual, estado);
        }

        public string[] retornarDatos(int artId) {
            ArticuloModel = new ArticuloModel();
            return ArticuloModel.retornarDatos(artId);

        }

        public List<string> retornarAutor(int artId)
        {
            ArticuloModel = new ArticuloModel();
            return ArticuloModel.retornarAutor(artId);

        }

        public void MarcarArtSolicitado(int artID) {
            ArticuloModel = new ArticuloModel();
            ArticuloModel.MarcarArticuloSolicitado(artID);

        }

        public void AsignarArticulo(int articuloId, List<String> revisores) {
            ArticuloModel = new ArticuloModel();
            ArticuloModel.AsignarArticulo(articuloId, revisores);
        }

        public List<List<String>> RetornarResultadoSolicitud(int articuloId) {
            ArticuloModel = new ArticuloModel();
            return ArticuloModel.RetornarResultadoSolicitud(articuloId);
        }

        public void ModificarEstadoSolicitud(int artID, string nombreUsuarioActual, string estadoSolicitud)
        {
            ArticuloModel = new ArticuloModel();
            ArticuloModel.ModificarEstadoSolicitud(artID, nombreUsuarioActual, estadoSolicitud);

        }
    }
}