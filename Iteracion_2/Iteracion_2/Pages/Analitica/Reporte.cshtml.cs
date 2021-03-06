﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.DataTable.Net.Wrapper.Extension;
using Google.DataTable.Net.Wrapper;
using Iteracion_2.Controllers;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Iteracion_2.Pages.Analitica
{
    public class ReporteModel : PageModel
    {
        public List<List<string>> ValoresGrafica { set; get; }

        ReporteController ControladorDistribucion { get; set; }

        public ActionResult OnGetOpciones(string seleccion, string tipo)
        {
            ControladorDistribucion = new ReporteController();
            string reporte = ControladorDistribucion.ComunicarSeleccion(seleccion, tipo);
            string jsonString = JsonSerializer.Serialize(reporte);

            return Content(reporte);
        }

        public ActionResult OnGetChartData(string entrada, string tipo)
        {
            ControladorDistribucion = new ReporteController();
            string FiltrosSeleccionados = entrada.TrimEnd(new Char[] { ',' });
            string[] selecciones = FiltrosSeleccionados.Split(',');

            var reporte = ControladorDistribucion.ComunicarDatosDistrubucion(selecciones, tipo);

            var json = reporte.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Tipo"), x => x.Entrada)
                    .NewColumn(new Column(ColumnType.Number, "Cantidad"), x => x.Cantidad)
                    .Build()
                    .GetJson();

            return Content(json);
        }
    }
}