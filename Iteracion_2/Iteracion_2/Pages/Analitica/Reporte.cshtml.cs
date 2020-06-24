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

        public void OnGet()
        {

        }

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
                //List<List<string>> ValoresGrafico = new List<List<string>>();
                ValoresGrafica = ControladorDistribucion.ComunicarDatosDistrubucion(Selecciones);

                string[] temp = new string[ValoresGrafica.Count];

                for (int i=0; i<ValoresGrafica.Count; i++) {
                    temp.Append(ValoresGrafica[i][0], ValoresGrafica[i][1])
                }

                var paises = new[]
                {
                    new {Name = "Costa Rica", Count = 2},
                    new {Name = "USA", Count = 1},
                    new {Name = "Korea", Count = 6},
                    new {Name = "Canada", Count = 3},
                    new {Name = "Peru", Count = 6}
                };


                //for (int i = 0; i < ValoresGrafica.Count; i++)
                //{
                //    paises = new[]
                //    {
                //        new {Name = ValoresGrafica[i][0], Count = Int16.Parse(ValoresGrafica[i][1])}
                //    };
                //}

                json = paises.ToGoogleDataTable()
                        .NewColumn(new Column(ColumnType.String, "Paises"), x => x.Name)
                        .NewColumn(new Column(ColumnType.Number, "Cantidad"), x => x.Count)
                        .Build()
                        .GetJson();
            }

            

            return Content(json);
        }
    }
}