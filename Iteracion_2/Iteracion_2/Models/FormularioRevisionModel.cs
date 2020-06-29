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

        public bool ValidarEntradas(string opinion, string contribucion, string forma)
        {
            bool validado = true;
            if (opinion.Equals("") || contribucion.Equals("") || forma.Equals(""))
            {
                validado = false;

            }
            return validado;

        }
        public bool ValidarObservaciones(string observaciones)
        {
            bool retorno = true;
            var regexInt = new System.Text.RegularExpressions.Regex(@"\b(?i)(UPDATE|DELETE FROM|SELECT|INSERT INTO|DROP)(?-i)\b");
            if (regexInt.IsMatch(observaciones))
            {
                retorno = false;
            }
            return retorno;

        }
        public bool ProcesarFormulario(int opinion, int contribucion, int forma, string observaciones, string miembroID, string artID)
        {
            bool validado = false;
            if (ValidarObservaciones(observaciones)) {
                validado = true;
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


                if (miembroID != "Coordinador")
                {
                    // Despues se guardan la nota y los comentarios con un estado de Revisado
                    ConexionBD = new ConexionModel();
                    SqlConnection con2 = ConexionBD.Connection();
                    SqlCommand cmd = new SqlCommand("UPDATE Nucleo_Revisa_Articulo SET estadoRevision= 'revisado',puntuacion=  " + Puntuacion + ", comentarios = '" + observaciones + "' WHERE nombreUsuarioFK = '" + miembroID + "'AND artIdFK = '" + artID + "'", con2);
                    cmd.ExecuteNonQuery();
                    con2.Close();
                } else
                {
                    // Despues se guardan la nota y los comentarios con un estado de Revisado
                    ConexionBD = new ConexionModel();
                    SqlConnection con2 = ConexionBD.Connection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO  Nucleo_Revisa_Articulo VALUES ('" + miembroID + "','" + artID + "', 'revisado'," + Puntuacion + ", '" + observaciones + "'  )", con2);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("UPDATE Articulo SET estado = 'revision' WHERE artIdPK =" + artID, con2);
                    cmd.ExecuteNonQuery();
                    con2.Close();
                }
            }
            return validado;


        }


    }
}
