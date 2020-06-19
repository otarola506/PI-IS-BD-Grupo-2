using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteracion_2.Models
{
    public class ArticuloModel
    {
        private SqlConnection con;
        private ConexionModel connectionString { get; set; }

        private Email Email1 { get; set; }
        public void Connection()
        {
            connectionString = new ConexionModel();
            con = connectionString.Connection();
        }

        public List<String> RecuperarNombresUsuarioNucleo() {

            Connection();
            List<string> Results = new List<string>();
            string query = "SELECT nombreUsuarioPk FROM Miembro WHERE pesoMiembro = 5";
            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text
            };
            using (SqlDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    Results.Add(reader["nombreUsuarioPk"].ToString());
                }

            }
            con.Close();
            return Results;
        }

        public void MarcarArticuloSolicitado(int artIdPk)
        {
            List<String> nombresUsuarioNucleo = RecuperarNombresUsuarioNucleo();
            Connection();
            var query = "INSERT INTO Nucleo_Solicita_Articulo VALUES (@nombreUsuario,@artID,@estado)";
            using (SqlCommand cmd = new SqlCommand(query,con))
            {
                for (int index = 0; index < nombresUsuarioNucleo.Count; index++)
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombresUsuarioNucleo[index]);
                    cmd.Parameters.AddWithValue("@artID", artIdPk);
                    cmd.Parameters.AddWithValue("@estado", "solicitado");
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();


                }

            }
            con.Close();

        }   
        public List<List<string>> RetornarPendientes() {
            List<List<string>> ArticulosPendientes = new List<List<string>>();
            string queryString = "SELECT A.artIdPK,A.titulo,A.resumen,M.nombre,M.nombreUsuarioPK FROM Articulo A JOIN Miembro_Articulo MA ON A.artIdPK = MA.artIdFK JOIN Miembro M  ON M.nombreUsuarioPK  = MA.nombreUsuarioFK WHERE A.estado = 'pendiente' ORDER BY A.artIdPK";

            Connection();
            con.Open();

            SqlCommand command = new SqlCommand(queryString, con)
            {
                CommandType = CommandType.Text
            };

            DataTable dTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dTable);
            
            for (int index=0; index<dTable.Rows.Count; index++) {
                string idAnterior = "";
                string idActual = dTable.Rows[index][0].ToString(); //ardIdPK actual

                if (index > 0) {
                    idAnterior = dTable.Rows[index - 1][0].ToString(); //ardIdPK de la iteración pasada
                }

                if (idActual != idAnterior) {
                    DataRow[] datosDeArticulo = dTable.Select("artIdPK = " + idActual); // devuelve los autores con ese artIdPK
                    
                    string autores = "";
                    string usuarios = "";
                    
                    for (int indexJ=0; indexJ< datosDeArticulo.Length; indexJ++) {
                        autores += datosDeArticulo[indexJ][3];
                        usuarios += datosDeArticulo[indexJ][4];
                        if (indexJ < datosDeArticulo.Length-1) {
                            autores += ",";
                            usuarios += ",";
                        }
                    }

                    ArticulosPendientes.Add(new List<string> {
                                    dTable.Rows[index][0].ToString(), // artIdPK
                                        dTable.Rows[index][1].ToString(), // titulo
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
