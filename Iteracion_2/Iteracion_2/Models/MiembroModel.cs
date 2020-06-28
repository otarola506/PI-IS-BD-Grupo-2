using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Iteracion_2.Models;

namespace Iteracion_2.Models
{
    public class MiembroModel
    {
        private SqlConnection con;
        private ConexionModel connectionString { get; set; }
        public void Connection()
        {
            connectionString = new ConexionModel();
            con = connectionString.Connection();
        }

        public void CrearCuenta(string[] info) {
            Connection();
            SqlCommand cmd = new SqlCommand("CrearCuenta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = info[0];
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = info[1];
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = info[2];
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = info[3];
            cmd.Parameters.Add("@pais", SqlDbType.VarChar).Value = info[4];
            cmd.Parameters.Add("@idioma", SqlDbType.VarChar).Value = info[5];
            cmd.Parameters.Add("@hobbies", SqlDbType.VarChar).Value = info[6];
            cmd.Parameters.Add("@habilidades", SqlDbType.VarChar).Value = info[7];
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public virtual Boolean VerificarNombreUsuario(string nombreUsuario)
        {
            Connection();
            string verificacion = "";
            bool Existe = false;
            SqlCommand cmd = new SqlCommand("VerificarNombreUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                verificacion = reader[0].ToString();
            }

            if (verificacion.Equals("1")) {
                Existe = true;
            }

            con.Close();
            return Existe;
        }

        public List<List<string>>RetornarMiembros() {
            List<List<string>> miembrosComunidad = new List<List<string>>();

            Connection();

            string query = "SELECT M.nombreUsuarioPK, M.nombre+' '+M.apellido AS [Nombre Completo], M.correo, M.merito, M.pesoMiembro FROM dbo.Miembro M ORDER BY M.pesoMiembro DESC, M.nombre ASC";
            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text

            };
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    miembrosComunidad.Add(new List<string>
                                        {
                                            reader[0].ToString(), // nombreUsuarioPK
                                            reader[1].ToString(), // nombre
                                            reader[2].ToString(), // correo
                                            reader[3].ToString(), // merito 
                                            reader[4].ToString(), // peso 
                                        });
                }

            }
            con.Close();

            return miembrosComunidad;
        }

        public bool ValidarUsuarioInyeccion(string NombreUsuario)
        {
            bool retorno = false;
            var regexInt = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]*$");
            if (regexInt.IsMatch(NombreUsuario)) {
                retorno = true;
            }
            return retorno;

        }

        public bool IngresarCuenta(string NombreUsuario)
        {
            // En este momento solo nos importaba validar el nombre de usuario, en un futuro deberemos de validar la contraseña
            bool Existe = false;
            if (ValidarUsuarioInyeccion(NombreUsuario))
            {
                Connection();
                string verificacion = "";
                SqlCommand cmd = new SqlCommand("VerificarNombreUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    verificacion = reader[0].ToString();
                }

                if (verificacion.Equals("1"))
                {
                    Existe = true;
                }
                con.Close();
            }
            return Existe;
        }

        public (string,string) RetornarPesoMiembroTipo(string NombreUsuario) {
            Connection();
            SqlCommand cmd = new SqlCommand("RecuperarPesoMiembro", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
            SqlDataReader reader = cmd.ExecuteReader();

            string peso = "";
            string tipo = "";
            while (reader.Read())
            {
                peso = reader[0].ToString();
                tipo = reader[1].ToString();
            }

            con.Close();
            return (peso,tipo);
        }



        public List<String> RecuperarCorreosMiembros()
        {
            Connection();
            List<string> Results = new List<string>();
            string query = "SELECT correo FROM Miembro";
            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text
            };
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Results.Add(reader["correo"].ToString());
                }

            }
            con.Close();
            return Results;




        }

    }

    
}
