using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iteracion_2.Models;


namespace PruebasComunidadPractica
{
    [TestClass]
    public class UnitTestMiembroModel
    {
        [TestMethod]
        public void TestIngresarCuenta()
        {
            MiembroModel miembro;
            miembro = new MiembroModel();
            string nombreUsuario = "otarola506";
            
            Assert.IsTrue(miembro.IngresarCuenta(nombreUsuario) == true,"Usuario no esta registrado en la página");

        }
    }
}
