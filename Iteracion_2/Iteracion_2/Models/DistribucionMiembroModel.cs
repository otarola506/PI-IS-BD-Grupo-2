using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Iteracion_2.Models
{
    public class ReporteConfigurable {
        public string Entrada { get; set; }
        public string Cantidad { get; set; }

        public string Entrada2  { get; set; }

        public string Cantidad2 { get; set; }


    }

    public class DistribucionMiembroModel
    {
        ConexionModel ConexionBD { get; set; }
        private SqlConnection con;

        public void Connection()
        {
            ConexionBD = new ConexionModel();
            con = ConexionBD.Connection();
        }


        public List<ReporteConfigurable> EncontrarValoresDistribucion(string[] Valores)
        {
            List<ReporteConfigurable> Retorno = new List<ReporteConfigurable>();
            Connection();
            if (Valores.Length > 1)
            {
                //Busqueda por mas de un valor
                // hacemos la consulta avanzada  para los nombres que queremos en la grafica
                //string AtributoDistribucion = "M." + Valores[0] + ", M."+Valores[1]+"";
                //string QueryString = "SELECT @Valor1, COUNT(@Valor1), @Valor2, COUNT(@Valor2) FROM Miembro GROUP BY "+ AtributoDistribucion + "";
                //using (SqlCommand command = new SqlCommand(QueryString, con))
                //{

                //    command.Parameters.AddWithValue("@Valor1", Valores[0]);
                //    command.Parameters.AddWithValue("@Valor2", Valores[1]);
                //    SqlDataReader reader = command.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        var reporte = new ReporteConfigurable { Entrada = reader[0].ToString(), Cantidad = reader[1].ToString(), Entrada2 = reader[2].ToString(), Cantidad2 = reader[3].ToString() };
                //        Retorno.Add(reporte);
                //    }

                //}


                string AtributoDistribucion = "M." + Valores[0] + ", M."+Valores[1]+"";
                string QueryString = "SELECT @Valor1, COUNT(@Valor1), @Valor2, COUNT(@Valor2) FROM Miembro M GROUP BY " + AtributoDistribucion + "";
                SqlCommand command = new SqlCommand(QueryString, con)
                {
                    CommandType = CommandType.Text
                };

                command.Parameters.AddWithValue("@Valor1", Valores[0]);
                command.Parameters.AddWithValue("@Valor2", Valores[1]);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reporte = new ReporteConfigurable { Entrada = reader[0].ToString(), Cantidad = reader[1].ToString(), Entrada2 = reader[2].ToString(), Cantidad2 = reader[3].ToString() };
                        Retorno.Add(reporte);
                    }

                }



            }
            else if (Valores.Length == 1) //Busqueda por un unico valor
            {
                // hacemos la consulta avanzada  para los nombres que queremos en la grafica
                string AtributoDistribucion = "M."+ Valores[0] +"";
                SqlCommand command = new SqlCommand("SELECT "+AtributoDistribucion+", COUNT(*)   FROM Miembro M GROUP BY "+AtributoDistribucion+"", con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var reporte = new ReporteConfigurable { Entrada = reader[0].ToString(), Cantidad = reader[1].ToString() };
                    Retorno.Add(reporte);
                }
                
            }
            con.Close();
            return Retorno;
        }


    }
}
