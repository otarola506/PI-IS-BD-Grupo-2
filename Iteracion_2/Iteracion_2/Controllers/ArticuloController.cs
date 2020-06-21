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

        public string[] retornarDatos(int artId) {
            ArticuloModel = new ArticuloModel();
            return ArticuloModel.retornarDatos(artId);

        }
    }
}