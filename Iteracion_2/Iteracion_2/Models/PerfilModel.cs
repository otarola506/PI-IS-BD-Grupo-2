using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteracion_2.Models
{
    public class PerfilModel
    {
        private SqlConnection con;
        private ConexionModel ConnectionString { get; set; }

        public void Connection()
        {
            ConnectionString = new ConexionModel();
            con = ConnectionString.Connection();
        }

        public List<String> RetornarDatosPerfil(string nombreUsuario)
        {
            List<String> informacionPersonal = new List<String>();

            string query = "SELECT M.nombre,M.apellido, M.pesoMiembro, M.informacionLaboral, M.informacionBiografica, M.telefono, M.correo, M.merito, M.pais, M.habilidades, M.idiomas, M.hobbies FROM dbo.Miembro M WHERE M.nombreUsuarioPK = @nombreUsuario";

            Connection();

            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text
                
            };

            command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int index = 0; index < 12; index++)
                    {

                        if (index == 2)
                        {
                            string pesoMiembro = reader[index].ToString();
                            string pesoComunidad = "";
                            if (pesoMiembro == "3")
                            {
                                pesoComunidad = "Activo";
                            }
                            else if (pesoMiembro == "5")
                            {
                                pesoComunidad = "Núcleo";
                            }
                            else
                            {
                                pesoComunidad = "Periférico";
                            }

                            informacionPersonal.Add(pesoComunidad);
                        }
                        else
                        {
                            informacionPersonal.Add(reader[index].ToString());
                        }
                    }
                }

            }

            con.Close();

            return informacionPersonal;
        }

        public List<List<string>> RetornarArticulosMiembro(string nombreUsuario)
        {
            string queryIds = "SELECT A.artIdPK FROM Articulo A JOIN Miembro_Articulo MA ON A.artIdPK = MA.artIdFK JOIN Miembro M  ON M.nombreUsuarioPK  = MA.nombreUsuarioFK WHERE M.nombreUsuarioPK = @nombreUsuario ORDER BY A.artIdPK";
            
            Connection();

            SqlCommand commandIds = new SqlCommand(queryIds, con)
            {
                CommandType = CommandType.Text

            };
            commandIds.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

            List<int> ids = new List<int>();

            using (SqlDataReader reader = commandIds.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids.Add(Int16.Parse(reader[0].ToString() ));
                }
            }

            return RetornarEspecificos(ids);
        }

        private List<List<string>> RetornarEspecificos(List<int> ids) {
            List<List<string>> ArticulosAutor = new List<List<string>>();
            string queryArticulos = "SELECT A.artIdPK,A.titulo,A.resumen,M.nombre+' '+M.apellido AS [Nombre Completo],M.nombreUsuarioPK FROM Articulo A JOIN Miembro_Articulo MA ON A.artIdPK = MA.artIdFK JOIN Miembro M  ON M.nombreUsuarioPK  = MA.nombreUsuarioFK WHERE A.artIdPK = @artIdPK ORDER BY A.artIdPK";

            for (int indexId = 0; indexId < ids.Count; indexId++)
            {
                SqlCommand commandArticulos = new SqlCommand(queryArticulos, con)
                {
                    CommandType = CommandType.Text
                };
                commandArticulos.Parameters.AddWithValue("@artIdPK", ids[indexId]);
                DataTable dTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(commandArticulos);
                adapter.Fill(dTable);

                for (int index = 0; index < dTable.Rows.Count; index++)
                {
                    string idAnterior = "";
                    string idActual = dTable.Rows[index][0].ToString(); //ardIdPK actual

                    if (index > 0)
                    {
                        idAnterior = dTable.Rows[index - 1][0].ToString(); //ardIdPK de la iteración pasada
                    }

                    if (idActual != idAnterior)
                    {
                        DataRow[] datosDeArticulo = dTable.Select("artIdPK = " + idActual); // devuelve los autores con ese artIdPK

                        string autores = "";
                        string usuarios = "";

                        for (int indexJ = 0; indexJ < datosDeArticulo.Length; indexJ++)
                        {
                            autores += datosDeArticulo[indexJ][3];
                            usuarios += datosDeArticulo[indexJ][4];
                            if (indexJ < datosDeArticulo.Length - 1)
                            {
                                autores += ",";
                                usuarios += ",";
                            }
                        }

                        ArticulosAutor.Add(new List<string>
                                {
                                    dTable.Rows[index][0].ToString(),
                                    dTable.Rows[index][1].ToString(),
                                    dTable.Rows[index][2].ToString(),
                                    autores,
                                    usuarios 
                                });
                    }
                }
            }
            con.Close();

            return ArticulosAutor;
        }

        public void GuardarDatosPerfil(string nombreUsuario, string[] informacionActualizada)
        {
            string query = "UPDATE dbo.Miembro SET nombre = @nombre, apellido = @apellido, informacionLaboral = @informacionLaboral, informacionBiografica = @informacionBiografica, telefono = @telefono, correo = @correo, pais = @pais, habilidades = @habilidades, idiomas = @idiomas, hobbies = @hobbies WHERE nombreUsuarioPK = @nombreUsuario";

            Connection();

            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text

            };

            command.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = informacionActualizada[0];
            command.Parameters.Add("@apellido", SqlDbType.VarChar).Value = informacionActualizada[1];
            command.Parameters.Add("@informacionLaboral", SqlDbType.VarChar).Value = informacionActualizada[2];
            command.Parameters.Add("@informacionBiografica", SqlDbType.VarChar).Value = informacionActualizada[3];
            command.Parameters.Add("@telefono", SqlDbType.VarChar).Value = informacionActualizada[4];
            command.Parameters.Add("@correo", SqlDbType.VarChar).Value = informacionActualizada[5];
            command.Parameters.Add("@pais", SqlDbType.VarChar).Value = informacionActualizada[6];
            command.Parameters.Add("@habilidades", SqlDbType.VarChar).Value = informacionActualizada[7];
            command.Parameters.Add("@idiomas", SqlDbType.VarChar).Value = informacionActualizada[8];
            command.Parameters.Add("@hobbies", SqlDbType.VarChar).Value = informacionActualizada[9];

            command.ExecuteNonQuery();

            con.Close();
        }

        public List<string> recuperarCorreos()
        {
            Connection();

            SqlCommand cmd = new SqlCommand("Recuperar_Correos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List<string> results = new List<string>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    results.Add(reader["correo"].ToString());
            }
            con.Close();
            return results;

        }

        public bool verificarCorreo(string Usuario)
        {
            Connection();

            string verificacion = "";
            bool Existe = true;
            SqlCommand cmd = new SqlCommand("ObtenerCorreo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = Usuario;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                verificacion = reader[0].ToString();
            }

            if (verificacion.Equals(""))
            {
                Existe = false;
            }

            return Existe;
        }

        public string obtenerCorreo(string Usuario)
        {
            Connection();

            string correo = "";
            SqlCommand cmd = new SqlCommand("ObtenerCorreo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = Usuario;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                correo = reader[0].ToString();
            }
            return correo;
        }
    }
}

