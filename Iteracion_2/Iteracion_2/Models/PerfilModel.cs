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
        public void Connection()
        {
            string conString = @"Server=172.16.202.75;Database=BD_Grupo2;persist security info=True;MultipleActiveResultSets=True;User ID=Grupo2;Password=grupo2.";
            con = new SqlConnection(conString);
        }

        public string[] RetornarDatosPerfil(string nombreUsuario)
        {
            Connection();
            con.Open();
            List<string> informacionPersonal = new List<string>();

            SqlCommand cmd = new SqlCommand("RetornarDatosPerfil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {

                for (int index = 0; index < 7; index++) {

                    if (index == 1)
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
                    else {
                        informacionPersonal.Add(reader[index].ToString());
                    }
                }
            }
            
            con.Close();

            return informacionPersonal.ToArray(); 
        }
    }
}
