using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace Iteracion_2.Models
{
    public class ConexionBD
    {
        private SqlConnection con;
        public void connection()
        {
            string conString = @"Server=172.16.202.75;Database=BD_Grupo2;persist security info=True;MultipleActiveResultSets=True;User ID=Grupo2;Password=grupo2.";
            con = new SqlConnection(conString);

        }


        //Metodo que se encarga de retornar un string con el estado del articulo
        //Parametro: El artID del articulo el cual se quiere revisar
        public string revisarEstadoArticulo(int artID)
        {
            con.Open();
            string valor = "";
            SqlCommand cmd = new SqlCommand("RecuperarEstadoArticulo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@artID", SqlDbType.Int).Value = artID; // le pasamos el artID
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                valor = reader[0].ToString();
            }
            reader.Close();

            con.Close();
            return valor;
        }

        public void modificarMeritoAutores(int artId, float puntuacion)
        {
            con.Open();
            string miembroID = "";
            SqlCommand cmd2 = new SqlCommand("RecuperarAutores", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@artID", SqlDbType.Int).Value = artId; 
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                miembroID = reader[0].ToString(); // Guardo el valor del nombre de usuario
                asignarPuntajeInicialMiembro(miembroID, puntuacion);// para cada autor ocupo que guarde el merito en puntaje
            }
            reader.Close();
            con.Close();
            
        }

        //Metodo que se encarga de asignarle el puntaje inicial al autor que se le pasa por parametro
        public void asignarPuntajeInicialMiembro(string miembroID, float puntuacion)
        {
            //Se llama al proc que guarda el puntaje en el perfil de la persona
            //con.Open();
            SqlCommand cmd = new SqlCommand("ingresarPuntuacionAutor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@puntuacion", SqlDbType.Float).Value = puntuacion;
            cmd.Parameters.Add("@miembroID", SqlDbType.VarChar).Value = miembroID;
            cmd.ExecuteNonQuery();
            //con.Close();


        }

        //Metodo que primero revisa si el artID esta aprobado, luego obtiene la puntuacion del articulo y lo asigna
        public void asignarMeritoPuntuacionInicial(int artID)
        {
            string estado = revisarEstadoArticulo(artID);
            if (estado == "aceptado")// Para futuras historias poner otros estados
            {
                //Obtenemos la puntacion del articulo
                con.Open();
                string puntuacion = "";
                SqlCommand cmd = new SqlCommand("RecuperarPuntuacionArticulo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@artID", SqlDbType.Int).Value = artID;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    puntuacion = reader[0].ToString();
                }

                reader.Close();
                con.Close();

                float valor = (float)Convert.ToDouble(puntuacion);

                //obtemos los autores y asignamos 
                modificarMeritoAutores(artID, valor);


                

            }



        }



    }
}