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

namespace Iteracion_2.Pages.Analitica
{
    public class Chart
    {
        public object[] cols { get; set; }
        public object[] rows { get; set; }
    }

    public class ReporteModel : PageModel
    {

        public List<List<string>> ValoresGrafica { set; get; }

        DistribucionMiembroController ControladorDistribucion { get; set; }

        public void OnGet() { }

        public ActionResult OnGetChartData(string entrada)
        {
            var json = "";

            if (entrada == null)
            {
                string[] seleccion = { "tipo" };
                ControladorDistribucion = new DistribucionMiembroController();

                var reporte = ControladorDistribucion.ComunicarDatosDistrubucion(seleccion);

                json = reporte.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String, "Tipo Miembro"), x => x.Entrada)
                        .NewColumn(new Column(ColumnType.Number, "Cantidad"), x => x.Cantidad)
                        .Build()
                        .GetJson();
            }
            else {

                
                string FiltrosSeleccionados = entrada.TrimEnd(new Char[] { ',' });

                
                string[] Selecciones = FiltrosSeleccionados.Split(',');
                ControladorDistribucion = new DistribucionMiembroController();

                var reporte = ControladorDistribucion.ComunicarDatosDistrubucion(Selecciones);

                json = reporte.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String, "Paises"), x => x.Entrada)
                        .NewColumn(new Column(ColumnType.Number, "Cantidad"), x => x.Cantidad)
                        .NewColumn(new Column(ColumnType.String, "Paises1"), x => x.Entrada2)
                        .NewColumn(new Column(ColumnType.Number, "Cantidad1"), x => x.Cantidad2)
                        .Build()
                        .GetJson();
            }

            return Content(json);
        }
    }
}