using System;
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
            string jsonString;
            jsonString = JsonSerializer.Serialize(reporte);

            return Content(reporte);
        }

        public ActionResult OnGetChartData(string entrada, string tipo)
        {
            var json = "";

            if (entrada == null)//onget
            {
                ControladorDistribucion = new ReporteController();

                var reporte = ControladorDistribucion.ComunicarDatosDistrubucion(entrada.Split(","), tipo);

                json = reporte.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String, "Tipo Miembro"), x => x.Entrada)
                        .NewColumn(new Column(ColumnType.Number, "Cantidad"), x => x.Cantidad)
                        .Build()
                        .GetJson();
            }
            else {
                string FiltrosSeleccionados = entrada.TrimEnd(new Char[] { ',' });
                string[] selecciones = FiltrosSeleccionados.Split(',');

                ControladorDistribucion = new ReporteController();

                var reporte = ControladorDistribucion.ComunicarDatosDistrubucion(selecciones, tipo);

                json = reporte.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String,"Tipo"), x => x.Entrada)
                        .NewColumn(new Column(ColumnType.Number,"Cantidad"), x => x.Cantidad)
                        .Build()
                        .GetJson();
            }

            return Content(json);
        }
    }
}