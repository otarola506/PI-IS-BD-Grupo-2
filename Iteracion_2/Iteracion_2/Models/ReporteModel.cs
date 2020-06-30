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
            string query = "";

            if (tipo == "simple")
                query = "SELECT DISTINCT " + seleccion + " FROM Miembro";
            else if ( tipo == "avanzado")
                query = "SELECT nombre FROM Topico";

            string listaOpciones = "";
            Connection();
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listaOpciones += reader[0].ToString() +",";
            }

            return listaOpciones;
        }

        public List<ReporteConfigurable> EncontrarValoresDistribucion(string[] valores, string tipo)
        {
            List<ReporteConfigurable> Retorno = new List<ReporteConfigurable>();

            string query = "";
            Connection();

            if (tipo == "simple")
            {
                query = RetornarSimple(valores);
            }
            else if (tipo == "avanzado")
            {
                query = RetornarAvanzado(valores);
            }

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

            con.Close();

            return Retorno;
        }

        private string RetornarSimple(string[] valores)
        {
            List<ReporteConfigurable> temp = new List<ReporteConfigurable>();
            string query = "";

            if (valores.Length > 1)
            {
                query = "SELECT " + valores[0] + " ,COUNT(*) FROM Miembro  WHERE " + valores[1] + " LIKE '%" + valores[2] + "%' GROUP BY (" + valores[0] + ")";
            }
            else {
                query = "SELECT " + valores[0] + ", COUNT(*)   FROM Miembro GROUP BY " + valores[0] + "";
            }

            return query;
        }

        private string RetornarAvanzado(string[] valores) {
            string query = "";

            if (valores.Length > 1)
            {
                string condicion = "";
                if (valores[0] == "accesos")
                {
                    condicion = "AVG(A.visitas) AS 'visitas por topico' ";

                }
                else if (valores[0] == "topico")
                {
                    condicion = "Count(T.nombre) as' Cantidad por topico'";
                }

                query = "SELECT  M.tipo, " + condicion + " FROM Miembro M ,Articulo A, Miembro_Articulo MA, Art_Topico ArT, Topico t WHERE M.nombreUsuarioPK  = MA.nombreUsuarioFK AND MA.artIdFK = A.artIdPK AND A.artIdPK = ArT.artIdFK AND ArT.topicoIdFK = T.topicoIdPK AND T.nombre = '" + valores[1] + "'  GROUP BY M.tipo";
            }
            else {
                string condicion = "";
                if (valores[0] == "cantidad articulos")
                    condicion = "COUNT(M.tipo) AS'Cantidad'";
                else if (valores[0] == "puntaje promedio")
                    condicion = "AVG(A.puntuacionInicial) AS'Puntuacion Promedio'";

                query = "SELECT M.tipo, " + condicion + " FROM Miembro M ,Articulo A, Miembro_Articulo MA WHERE M.nombreUsuarioPK  = MA.nombreUsuarioFK AND MA.artIdFK = A.artIdPK GROUP BY M.tipo";
            }

            return query;
        }
    }
}
