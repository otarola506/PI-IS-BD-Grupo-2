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

        public ActionResult OnGetChartData(string val)
        {
            var json = "";


            if (val == null)
            {
                var pizza = new[]
                {
                    new {Name = "Mushrooms", Count = 3},
                    new {Name = "Onions", Count = 1},
                    new {Name = "Olives", Count = 1},
                    new {Name = "Zucchini", Count = 1},
                    new {Name = "Pepperoni", Count = 2}
                };

                json = pizza.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String, "Topping"), x => x.Name)
                        .NewColumn(new Column(ColumnType.Number, "Slices"), x => x.Count)
                        .Build()
                        .GetJson();
            }
            else {
                string[] Selecciones = new string[2];
                Selecciones = val.Split(',');
                ControladorDistribucion = new DistribucionMiembroController();

                var reporte = ControladorDistribucion.ComunicarDatosDistrubucion(Selecciones);

                json = reporte.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String, "Paises"), x => x.Pais)
                        .NewColumn(new Column(ColumnType.Number, "Cantidad"), x => x.Cantidad)
                        .Build()
                        .GetJson();
            }

            return Content(json);
        }
    }
}