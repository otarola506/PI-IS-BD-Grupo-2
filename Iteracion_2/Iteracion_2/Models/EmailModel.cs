using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;


namespace Iteracion_2.Models
{
    public class EmailModel
    {
        private SqlConnection Con;
        private ConexionModel ConnectionString { get; set; }
        public void Connection()
        {
            ConnectionString = new ConexionModel();
            Con = ConnectionString.Connection();
        }
        public async Task EnviarCorreo(string destinatario, string asunto, string contenido,IFormFile archivo) {
            
            MailMessage mm = new MailMessage();
            mm.To.Add(destinatario);
            mm.Subject = asunto;
            AlternateView imgview = AlternateView.CreateAlternateViewFromString(contenido + "<br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
            LinkedResource lr = new LinkedResource(@"Images/shieldship.jpg", MediaTypeNames.Image.Jpeg)
            {
                ContentId = "imgpath"
            };
            imgview.LinkedResources.Add(lr);
            mm.AlternateViews.Add(imgview);
            mm.Body = lr.ContentId;
            if (archivo != null)
            {
                var ms = new MemoryStream();
                archivo.CopyTo(ms);
                var fileBytes = ms.ToArray();
                mm.Attachments.Add(new Attachment(new MemoryStream(fileBytes), archivo.FileName));
            }
            mm.IsBodyHtml = true;
            mm.From = new MailAddress("comunidadshieldship@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("comunidadshieldship@gmail.com", "BASESdatos176")
            };
            await smtp.SendMailAsync(mm);

        }

        public async Task EnviarSolicitud(string contenido,string Usuario)
        {
            await CorreoDefault("comunidadshieldship@gmail.com", "Solicitud de promocion de rango de " + Usuario, contenido);
        }

        public List<List<String>> RecuperarCorreos(int tipo, string usuario) {
            Connection();
            List<List<String>> Results = new List<List<String>>();
            string query = "";

            switch(tipo)
            {
                case 1:
                    query = "SELECT nombreUsuarioPK,correo FROM Miembro WHERE pesoMiembro = 5 AND tipo = 'nucleo'";
                    break;
                case 2:
                    query = "SELECT nombreUsuarioPK,correo FROM Miembro WHERE pesoMiembro = 5 AND nombreUsuarioPK='"+usuario+"'";
                    break;
                case 3:
                    query = "SELECT nombreUsuarioPK,correo FROM Miembro WHERE pesoMiembro = 5 AND tipo = 'coordinador'";
                    break;
                default:
                    break;

            }
            SqlCommand command = new SqlCommand(query, Con)
            {
                CommandType = CommandType.Text
            };
            DataTable dTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dTable);
            for (int index = 0; index < dTable.Rows.Count; index++)
            {
                Results.Add(new List<string> {
                                    dTable.Rows[index][0].ToString(), // nombreUsuario
                                        dTable.Rows[index][1].ToString(), // correo
                            });

            }

            Con.Close();
            return Results;
        }


        public async Task EnviarAsignacion(string titulo, string[] usuarios)
        {
            List<List<string>> CorreosNucleo = new List<List<string>>();
            for (int index=0; index<usuarios.Length; index++) {
                CorreosNucleo.Add(RecuperarCorreos(2,usuarios[index])[0]);
            }

            await EnviarCorreos("asignacion", titulo, CorreosNucleo);
        }

        public async Task EnviarSolicitudNucleo(string titulo)
        {
            List<List<String>> CorreosNucleo = RecuperarCorreos(1, null);

            await EnviarCorreos("solicitud", titulo, CorreosNucleo);
        }

        private async Task EnviarCorreos(string tipo, string titulo, List<List<string>> CorreosNucleo) {
            string correos= "";

            for (int index = 0; index < CorreosNucleo.Count; index++)
            {
                correos += CorreosNucleo[index][1]+";";
            }

            correos = correos.TrimEnd(new Char[] { ';' });

            string asunto= "";
            string mensaje = "";

            switch (tipo) {
                case "asignacion":
                    asunto = "Asignación de revisión: '"+ titulo+"'";
                    mensaje = "Estimado(s) miembro(s) núcleo, se le ha asignado para su revisión el artículo titulado '" + titulo + "'. Por favor revisarlo lo más antes posible.";
                    break;
                case "solicitud":
                    asunto = "Colaboración en el artículo '" + titulo+"'";
                    mensaje = "Estimados miembros núcleo, se le solicita la colaboración en el proceso de revisión del artículo '" + titulo+ "'.";
                    break;
            }

            await CorreoDefault(correos, asunto, mensaje);
        }

        

        public async Task CorreoDefault(string correos, string asunto, string mensaje)
        {
            MailMessage mailMessage = new MailMessage();

            foreach (var address in correos.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.To.Add(address);
            }

            mailMessage.Subject = asunto;
            AlternateView imgview = AlternateView.CreateAlternateViewFromString(mensaje + "<br/><br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
            LinkedResource lr = new LinkedResource(@"Images/shieldship.jpg", MediaTypeNames.Image.Jpeg)
            {
                ContentId = "imgpath"
            };
            imgview.LinkedResources.Add(lr);
            mailMessage.AlternateViews.Add(imgview);
            mailMessage.Body = lr.ContentId;
            mailMessage.IsBodyHtml = false;
            mailMessage.From = new MailAddress("comunidadshieldship@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("comunidadshieldship@gmail.com", "BASESdatos176")
            };
            await smtp.SendMailAsync(mailMessage);
        }

        public async Task CorreoACoordinadores(string titulo, string estadoSolicitud, string nombreUsuario)
        {
            List<List<String>> CorreosCoodinador = RecuperarCorreos(3, null);

            for (int index = 0; index < CorreosCoodinador.Count; index++)
            {
                await CorreoDefault(CorreosCoodinador[index][1],
                 "Respuesta a solicitud de revisión.",
                 "Estimado " + CorreosCoodinador[index][0] + ", se le informa que '" + nombreUsuario + "' ha '" + estadoSolicitud + "' la solicitud para la revisión del artículo titulado '" + titulo);
            }
        }

    }
}
