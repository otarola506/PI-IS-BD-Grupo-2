using Iteracion_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iteracion_2.Controllers
{
    public class EmailController
    {
        public EmailModel Correo { get; set; }
        public PerfilModel Perfil { get; set; }

        public MiembroModel Miembros { get; set; }

        public async Task enviarCorreo(string destinatario, string asunto, string contenido,IFormFile archivo) {
            Correo = new EmailModel();
            await Correo.EnviarCorreo(destinatario,asunto,contenido,archivo);
        }

        public bool verificarCorreo(string usuario)
        {
            Perfil = new PerfilModel();
            return Perfil.verificarCorreo(usuario);

        }

        public async Task enviarSolicitud(string contenido, string Usuario)
        {
            Correo = new EmailModel();
            await Correo.EnviarSolicitud(contenido, Usuario);
        }

        public List<string> recuperarCorreos() {
            Miembros= new MiembroModel();
            return Miembros.RecuperarCorreosMiembros();
        }

        public string obtenerCorreo(string usuario) {
            Perfil = new PerfilModel();
            return Perfil.obtenerCorreo(usuario);
        }

        public async Task CorreoANucleo(string titulo, string tipo, string[] usuarios) {
            Correo =  new EmailModel();

            switch (tipo) {
                case "solicitar":
                    await Correo.EnviarSolicitudNucleo(titulo);
                    break;
                case "asignar":
                    await Correo.EnviarAsignacion(titulo, usuarios);
                    break;
                default:
                    break;
            }            
        }

        public async Task CorreoACoordinadores(string titulo, string estado, string nombreUsuario)
        {
            Correo = new EmailModel();
            await Correo.CorreoACoordinadores(titulo, estado, nombreUsuario);
        }
    }
}
