using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            List<List<string>> informacionPersonal = new List<List<string>>();
            SqlDataReader reader = this.RetornarDatosDeUsuario(nombreUsuario, "Recuperar_Articulos_Autor");


            while (reader.Read())
            {
                informacionPersonal.Add(new List<string> { reader[0].ToString(), reader[1].ToString() });
            }

            con.Close();

            return informacionPersonal;
        }

        private SqlDataReader RetornarDatosDeUsuario(string nombreUsuario, string procedimiento)
        {
            Connection();

            SqlCommand cmd = new SqlCommand(procedimiento, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = nombreUsuario;
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
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

