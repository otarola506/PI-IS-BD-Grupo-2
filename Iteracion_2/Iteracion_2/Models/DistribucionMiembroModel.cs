using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Iteracion_2.Models
{
    public class DistribucionMiembroModel
    {
        ConexionModel ConexionBD { get; set; }
        private SqlConnection con;

        public void Connection()
        {
            ConexionBD = new ConexionModel();
            con = ConexionBD.Connection();
        }


        public List< List<string > > EncontrarValoresDistribucion(string[] Valores)
        {
            List<List<string>> Retorno = new List<List<string>>();
            if (Valores.Length > 1)
            {
                //Busqueda por mas de un valor





            }else if (Valores.Length == 1)
            {
                //Busqueda por un unico valor
                List<string> ListaIdentificadores = new List<string>();
                List<string> CuentaIdentificadores = new List<string>();



                // hacemos la consulta avanzada  para los nombres que queremos en la grafica

                Connection();
                string AtributoDistribucion = "M."+ Valores[0] +"";
                SqlCommand cmd1 = new SqlCommand("SELECT "+AtributoDistribucion+", COUNT(*)   FROM Miembro M GROUP BY "+AtributoDistribucion+"", con);
                //con.Open();
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    Retorno.Add( new List<string> { reader1[0].ToString() , reader1[1].ToString() });
                    
                }
                con.Close();
                reader1.Close();


                //Retorno.Add(ListaIdentificadores);
                //Retorno.Add(CuentaIdentificadores);
                

                
            }

            return Retorno;

        }


    }
}
