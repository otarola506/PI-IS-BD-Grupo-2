using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iteracion_2.Models;
using Iteracion_2.Controllers;
using Moq;

namespace PruebasComunidadPractica.Ingresar
{
    [TestClass]
    public class UnitTestIngresar
    {
        [TestMethod]
        public void VerificarDatosCorrectosInyeccion() {
            MiembroModel miembro;
            miembro = new MiembroModel();
            bool resultado = miembro.ValidarUsuarioInyeccion("otarola506");
            Assert.IsTrue(resultado, "Caracteres invalidos");


        }

        [TestMethod]
        public void VerificarDatosMaliciososInyeccion()
        {
            MiembroModel miembro;
            miembro = new MiembroModel();
            bool resultado = miembro.ValidarUsuarioInyeccion("otarola506; DELETE");
            Assert.IsFalse(resultado, "Caracteres aceptados");
        }

        [TestMethod] // Esta es la prueba que usa el interface se creo una clase MiembroModelT en donde esta defino
        public void ValidarNombreUsuarioDB()
        {

            var mockMiembro = new Mock<IMiembroModel>();
            mockMiembro.Setup(x => x.VerificarNombreUsuario("otarola506")).Returns(true);

            var miembro =new  MiembroModelT(mockMiembro.Object);
            bool resultado = miembro.VerificarNombreUsuario("otarola506");
            Assert.IsTrue(resultado, "Credenciales invalidas");

        }

    }
}

