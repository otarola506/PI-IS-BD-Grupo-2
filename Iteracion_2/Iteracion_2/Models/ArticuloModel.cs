using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Iteracion_2.Models
{
    public class ArticuloModel
    {
        private SqlConnection con;
        public void Connection()
        {
            string conString = @"Server=172.16.202.75;Database=BD_Grupo2;persist security info=True;MultipleActiveResultSets=True;User ID=Grupo2;Password=grupo2.";
            con = new SqlConnection(conString);
        }

        public List<List<string>> RetornarPendientes() {
            List<List<string>> ArticulosPendientes = new List<List<string>>();
            string queryString = "SELECT A.artIdPK,A.titulo,M.nombre,M.nombreUsuarioPK FROM Articulo A JOIN Miembro_Articulo MA ON A.artIdPK = MA.artIdFK JOIN Miembro M  ON M.nombreUsuarioPK  = MA.nombreUsuarioFK WHERE A.estado = 'pendiente' ORDER BY A.artIdPK";

            Connection();
            con.Open();

            SqlCommand command = new SqlCommand(queryString, con)
            {
                CommandType = CommandType.Text
            };

            DataTable dTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dTable);

            DataView view = new DataView(dTable);
            DataTable articulosIdDistintos = view.ToTable(true, "artIdPK");
            
            for (int index=0; index<dTable.Rows.Count; index++) {
                string idAnterior = "";
                string idActual = dTable.Rows[index][0].ToString(); //ardIdPK actual

                if (index > 0) {
                    idAnterior = dTable.Rows[index - 1][0].ToString(); //ardIdPK de la iteración pasada
                }

                if (idActual != idAnterior) {
                    DataRow[] autoresArticulo = dTable.Select("artIdPK = " + idActual); // devuelve los autores con ese artIdPK
                    DataRow[] usuarioAutores = dTable.Select("artIdPK = " + idActual); // devuelve los nombres de usuario con ese artIdPK

                    string autores = "";
                    string usuarios = "";
                    for (int indexJ=0; indexJ< autoresArticulo.Length; indexJ++) {
                        autores += autoresArticulo[indexJ][2];
                        usuarios += autoresArticulo[indexJ][3];
                        if (indexJ < autoresArticulo.Length-1) {
                            autores += ",";
                            usuarios += ",";
                        }
                    }

                    ArticulosPendientes.Add(new List<string> {
                                    dTable.Rows[index][0].ToString(),
                                    dTable.Rows[index][1].ToString(),
                                    autores,
                                    usuarios,
                            });
                }
            }

            con.Close();

            return ArticulosPendientes;
        }
    }
}
