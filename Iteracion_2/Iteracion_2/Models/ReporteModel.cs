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


        public string OptenerValoresSeleccion(string seleccion, string tipo)
        {
            List<ReporteConfigurable> Opciones = new List<ReporteConfigurable>();
            string Retorno= "";
            Connection();
            string AtributoDistribucion = "M." + seleccion + "";

            string query = "";

            if (tipo == "1")
                query = "SELECT DISTINCT " + AtributoDistribucion + " FROM Miembro  M";
            else
                query = "SELECT nombre FROM Topico";


            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Retorno += reader[0].ToString() +",";
            }

            return Retorno;
        }

        public List<ReporteConfigurable> EncontrarValoresDistribucion(string[] valores, string tipo)
        {
            List<ReporteConfigurable> Retorno = new List<ReporteConfigurable>();
            Connection();
            if (valores.Length > 1)
            {
                var query = "SELECT "+valores[0]+" ,COUNT(*) FROM Miembro  WHERE "+valores[1]+" = '"+valores[2]+"' GROUP BY ("+valores[0]+")";

                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reporte = new ReporteConfigurable { Entrada = reader[0].ToString(), Cantidad = reader[1].ToString() };
                        Retorno.Add(reporte);
                    }
                }

            }
            else if (valores.Length == 1) //Busqueda por un unico valor
            {
                string query = "";

                if (tipo == "simple") {
                    string AtributoDistribucion = "M." + valores[0] + "";
                    query = "SELECT " + AtributoDistribucion + ", COUNT(*)   FROM Miembro M GROUP BY " + AtributoDistribucion + "";

                }else
                {
                    string condicion = "";
                    if (valores[0] != "cantidad articulos")
                        condicion = "COUNT(M.tipo) AS'Cantidad'";
                    else
                        condicion = "AVG(A.puntuacionInicial) AS'Puntuacion Promedio'";
                    query = "SELECT M.tipo, " + condicion + " FROM Miembro M ,Articulo A, Miembro_Articulo MA WHERE M.nombreUsuarioPK  = MA.nombreUsuarioFK AND MA.artIdFK = A.artIdPK GROUP BY M.tipo";

                }

                SqlCommand command = new SqlCommand(query, con);
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
