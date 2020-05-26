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

    }
}
