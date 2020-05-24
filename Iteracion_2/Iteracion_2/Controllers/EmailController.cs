using Iteracion_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Iteracion_2.Controllers
{
    public class EmailController
    {
        [HttpPost]
        public async Task enviarCorreo(Email em) {
            string to = em.To;
            string subject = em.Subject;
            string body = em.Body;
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = subject;
            AlternateView imgview = AlternateView.CreateAlternateViewFromString(body + "<br/><img src=cid:imgpath height=200 width=400>", null, "text/html");
            LinkedResource lr = new LinkedResource(@"Images/shieldship.jpg", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "imgpath";
            imgview.LinkedResources.Add(lr);
            mm.AlternateViews.Add(imgview);
            mm.Body = lr.ContentId;
            if (em.archivo != null)
            {
                var ms = new MemoryStream();
                em.archivo.CopyTo(ms);
                var fileBytes = ms.ToArray();
                mm.Attachments.Add(new Attachment(new MemoryStream(fileBytes),em.archivo.FileName));
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
