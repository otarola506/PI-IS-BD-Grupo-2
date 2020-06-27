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

    public class ReporteModel
    {
        ConexionModel ConexionBD { get; set; }
        private SqlConnection con;

        public void Connection()
        {
            ConexionBD = new ConexionModel();
            con = ConexionBD.Connection();
        }


        public string OptenerValoresSeleccion(string Seleccion)
        {
            List<ReporteConfigurable> Opciones = new List<ReporteConfigurable>();
            string Retorno= "";
            Connection();
            string AtributoDistribucion = "M." + Seleccion + "";
      
            SqlCommand command = new SqlCommand("SELECT DISTINCT " + AtributoDistribucion + " FROM Miembro  M", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Retorno += reader[0].ToString() +",";
                //var reporte = new ReporteConfigurable { Entrada = reader[0].ToString()};
                //Opciones.Add(reporte);
            }


            //return Opciones ;
            return Retorno;
        }



        public List<ReporteConfigurable> EncontrarValoresDistribucion(string[] Valores)
        {
            List<ReporteConfigurable> Retorno = new List<ReporteConfigurable>();
            Connection();
            if (Valores.Length > 1)
            {
                // Busqueda por varios valores 
                //string AtributoDistribucion = "M." + Valores[0] + "";
                //string AtributoDistribucionAvanzada = "M."+Valores[1]+ "";
                //SqlCommand command = new SqlCommand("SELECT " + AtributoDistribucion + ", COUNT(*)   FROM Miembro WHERE "+AtributoDistribucionAvanzada+" = '"+Valores[2]+"' GROUP BY " + AtributoDistribucion + "", con);
                //SqlDataReader reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    var reporte = new ReporteConfigurable { Entrada = reader[0].ToString(), Cantidad = reader[1].ToString() };
                //    Retorno.Add(reporte);
                //}
                var query = "SELECT "+Valores[0]+" ,COUNT(*) FROM Miembro  WHERE "+Valores[1]+" = '"+Valores[2]+"' GROUP BY ("+Valores[0]+")";

                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.AddWithValue("@Valor1", Valores[0]);
                //cmd.Parameters.AddWithValue("@Valor2", Valores[1]);
                //cmd.Parameters.AddWithValue("@Valor3", Valores[2]);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reporte = new ReporteConfigurable { Entrada = reader[0].ToString(), Cantidad = reader[1].ToString() };
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
