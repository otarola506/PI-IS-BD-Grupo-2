using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iteracion_2.Models;

namespace PruebasComunidadPractica
{
    [TestClass]
    public class UnitTestIngresar
    {
        [TestMethod]
        public void VerificarDatosCorrectosInyeccion() {
            MiembroModel miembro;
            miembro = new MiembroModel();
            bool resultado = miembro.ValidarUsuario("otarola506");
            Assert.IsTrue(resultado, "Caracteres invalidos");


        }

        [TestMethod]
        public void VerificarDatosMaliciososInyeccion()
        {
            MiembroModel miembro;
            miembro = new MiembroModel();
            bool resultado = miembro.ValidarUsuario("otarola506; DELETE");
            Assert.IsFalse(resultado, "Caracteres aceptados");


        }
    }
}

