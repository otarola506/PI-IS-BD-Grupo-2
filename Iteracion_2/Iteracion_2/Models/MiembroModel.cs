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

        public Boolean verificarNombreUsuario(string nombreUsuario)
        {
            return true;
        }
    }
}
