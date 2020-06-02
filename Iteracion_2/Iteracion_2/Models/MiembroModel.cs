using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Iteracion_2.Models
{
    public class MiembroModel
    {
        private SqlConnection con;
        public void Connection()
        {
            string conString = @"Server=172.16.202.75;Database=BD_Grupo2;persist security info=True;MultipleActiveResultSets=True;User ID=Grupo2;Password=grupo2.";
            con = new SqlConnection(conString);
            con.Open();
        }

        public void crearCuenta(string nombreUsuario, string nombre, int peso) {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CrearCuenta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@peso", SqlDbType.VarChar).Value = peso;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void crearPerfil(string nombreUsuario, string info,float merito)
        {
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CrearPerfil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuarioFK", SqlDbType.VarChar).Value = nombreUsuario;
            cmd.Parameters.Add("@infoLaboral", SqlDbType.VarChar).Value = info;
            cmd.Parameters.Add("@infoBiografico", SqlDbType.VarChar).Value = info;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = info;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = info;
            cmd.Parameters.Add("@merito", SqlDbType.VarChar).Value = merito;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Boolean verificarNombreUsuario(string nombreUsuario)
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

        public (List<List<string>>, string ) RetornarMiembros(string NombreUsuario) {
            List<List<string>> miembrosComunidad = new List<List<string>>();

            Connection();

            string PesoUsuarioActual = "";
            SqlCommand cmd = new SqlCommand("RecuperarTodosUsuarios", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {
                if (reader[0].ToString() == NombreUsuario)
                {
                    PesoUsuarioActual = reader[4].ToString(); // peso 
                }
                miembrosComunidad.Add(new List<string>
                                        {
                                            reader[0].ToString(), // nombreUsuarioPK
                                            reader[1].ToString(), // nombre
                                            reader[2].ToString(), // correo
                                            reader[3].ToString(), // merito 
                                            reader[4].ToString(), // peso 
                                        });
            }

            con.Close();

            return (miembrosComunidad, PesoUsuarioActual );
        }


        public bool IngresarCuenta(string NombreUsuario)
        {
            // En este momento solo nos importaba validar el nombre de usuario, en un futuro deberemos de validar la contraseña
            Connection();
            string verificacion = "";
            bool Existe = false;
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
            return Existe;
        }

        public string RetornarPesoMiembro(string NombreUsuario) {
            Connection();
            SqlCommand cmd = new SqlCommand("RecuperarPesoMiembro", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
            SqlDataReader reader = cmd.ExecuteReader();

            string peso = "";
            while (reader.Read())
            {
                peso = reader[0].ToString();
            }

            con.Close();
            return peso;
        }

    }
}
