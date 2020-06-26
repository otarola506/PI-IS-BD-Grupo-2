using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Models
{
    public class FormularioRevisionModel
    {
        ConexionModel ConexionBD { get; set; }

        public void ProcesarFormulario(int opinion, int contribucion, int forma, string observaciones, string miembroID, string artID)
        {

            //Primeo se calcula la nota
            int suma = opinion + contribucion + forma;

            //Obtener el merito del miembro
            ConexionBD = new ConexionModel();
            SqlConnection con = ConexionBD.Connection();

            int Peso = 0; // cambiar esto

            string PesoMiembro = "";
            SqlCommand cmd1 = new SqlCommand("SELECT M.pesoMiembro FROM Miembro M WHERE M.nombreUsuarioPK = '" + miembroID + "'", con);
            //con.Open();
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {
                PesoMiembro = reader1[0].ToString();
            }
            con.Close();
            reader1.Close();
            
            Peso = Int16.Parse(PesoMiembro);

            int Puntuacion = suma * Peso;

            // Despues se guardan la nota y los comentarios con un estado de Revisado
            ConexionBD = new ConexionModel();
            SqlConnection con2 = ConexionBD.Connection();
            SqlCommand cmd = new SqlCommand("UPDATE Nucleo_Revisa_Articulo SET estadoRevision= 'Revisado',puntuacion=  "+Puntuacion+", comentarios = '"+observaciones+"' WHERE nombreUsuarioFK = '"+ miembroID + "'AND artIdFK = '"+ artID+"'",con2);
            cmd.ExecuteNonQuery();
            con2.Close();


        }


    }
}
