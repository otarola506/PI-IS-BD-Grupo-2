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
    public class Email
    {

        public async Task enviarCorreo(string destinatario, string asunto, string contenido,IFormFile archivo) {
            
            MailMessage mm = new MailMessage();
            mm.To.Add(destinatario);
            mm.Subject = asunto;
            AlternateView imgview = AlternateView.CreateAlternateViewFromString(contenido + "<br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
            LinkedResource lr = new LinkedResource(@"Images/shieldship.jpg", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "imgpath";
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
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("comunidadshieldship@gmail.com", "BASESdatos176");
            await smtp.SendMailAsync(mm);

        }

        public async Task enviarSolicitud(string contenido,string Usuario)
        {

            MailMessage mm = new MailMessage();
            mm.To.Add("comunidadshieldship@gmail.com");
            mm.Subject = "Solicitud de promocion de rango de " + Usuario;
            AlternateView imgview = AlternateView.CreateAlternateViewFromString(contenido + "<br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
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
            await smtp.SendMailAsync(mm);
        }

        //´Método que reciba nombre del artículo y lo que hace es mandar un correo a todos los miembros de núcleo.
        //public void enviarSolicitud()
        //Ocupo de fijo recuperar correos y pasarselos a este método para que se los envíe a todos

    }
}
