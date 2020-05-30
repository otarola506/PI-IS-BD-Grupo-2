using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Net.Mime;

namespace Iteracion_2.Models
{
    public class MeritoModel
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

        public string DegradarPeso(string nombreUsuario)
        {
            //Primero ocupo obtener el peso actual del usuario
            string mensaje = "";


            int peso = ObtenerPeso(nombreUsuario);
            if (peso == 3)
            {
                // Se le asigna el peso de 0
                ModificarPeso(nombreUsuario,0);
                ModificarMeritoPeso(3,0,nombreUsuario);
                NotificarMiembro(nombreUsuario,3,0);
                mensaje = "La operacion fue realizada con exito.";

            }else if (peso == 5)
            {
                //Se le asigna el peso de 3
                ModificarPeso(nombreUsuario,3);
                ModificarMeritoPeso(5, 3, nombreUsuario);
                NotificarMiembro(nombreUsuario, 5, 3);
                mensaje = "La operacion fue realizada con exito.";

            }
            else if (peso == 100)
            {
                // El mae metio algo que no era 
                mensaje = "El nombre que ingreso es incorrecto.";

            }else if (peso == 0)
            {
                mensaje = "Este miembro tiene el peso mas bajo, no se puede bajar mas.";
            }


            return mensaje;

        }

        


        public void NotificarMiembro( string Usuario, int pesoActual, int pesoNuevo)
        {
            //Primero ocupo encontrar el correo del miembro
            connection();
            string correoDestinatario = "";
            con.Open();
            SqlCommand cmd = new SqlCommand("ObtenerCorreo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = Usuario;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                correoDestinatario = reader[0].ToString();
            }

            reader.Close();
            con.Close();






            MailMessage mm = new MailMessage();
            mm.To.Add(correoDestinatario);
            mm.Subject = "Degradacion de peso en la comunidad " ;
            AlternateView imgview = AlternateView.CreateAlternateViewFromString("Su peso en la comunidad ha sido bajado de " + pesoActual + " a: " + pesoNuevo +". Esto afectara el merito de su perfil." 
                + "<br/><br/><br/><br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
            LinkedResource lr = new LinkedResource(@"Images/shieldship.jpg", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "imgpath";
            imgview.LinkedResources.Add(lr);
            mm.AlternateViews.Add(imgview);
            mm.Body = lr.ContentId;
            mm.IsBodyHtml = false;
            mm.From = new MailAddress("comunidadshieldship@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("comunidadshieldship@gmail.com", "BASESdatos176");
             smtp.SendMailAsync(mm);
        }



        public void ModificarMeritoPeso(int pesoActual,int pesoNuevo ,string NombreUsuario)
        {
            connection();

            // Se resta el actual 
            con.Open();
            SqlCommand cmd = new SqlCommand("DisminuirMeritoAutor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
            cmd.Parameters.Add("@aumento", SqlDbType.Int).Value = pesoActual;
            cmd.ExecuteNonQuery();

            con.Close();

            //Se suma el nuevo
            con.Open();
            SqlCommand cmd1 = new SqlCommand("AumentarMeritoAutor", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
            cmd1.Parameters.Add("@aumento", SqlDbType.Int).Value = pesoNuevo;
            cmd1.ExecuteNonQuery();
            con.Close();

        }

        public void ModificarPeso(string NombreUsuario,int nuevoPeso)
        {
            // Este metodo se puede utilizar para subir de rango o para bajar de rango(Peso)
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("ModificarPeso", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
            cmd.Parameters.Add("@peso", SqlDbType.Int).Value = nuevoPeso;
            cmd.ExecuteNonQuery();
            con.Close();

        }


        public int ObtenerPeso(string NombreUsuario)
        {
            connection();
            int PesoMiembro = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("RecuperarPesoMiembro", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar).Value = NombreUsuario;
            SqlDataReader reader = cmd.ExecuteReader();
            string Auxiliar = "";
            if (reader.Read())
            {
                Auxiliar = reader[0].ToString();
            }

            if (Auxiliar != "")
            {
                PesoMiembro = Convert.ToInt32(Auxiliar);
            }
            else
            {
                PesoMiembro = 100;
            }

            
           
            reader.Close();
            con.Close();



            return PesoMiembro;
        }

    }
}