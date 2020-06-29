using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iteracion_2.Models;


namespace PruebasComunidadPractica.FormularioRevision
{
    [TestClass]
    public class UnitTestFormularioRevision
    {
        
        [TestMethod]
        public void ValidarTodasEntradasUsuario()
        {
            FormularioRevisionModel form;
            form = new FormularioRevisionModel();
            bool resultado = form.ValidarEntradas("2","2","2");

            Assert.IsTrue(resultado, "Entradas invalidas");

            
        }

        [TestMethod]
        public void ValidarEntradasSinContribucion()
        {
            FormularioRevisionModel form;
            form = new FormularioRevisionModel();
            bool resultado = form.ValidarEntradas("2", "", "2");

            Assert.IsFalse(resultado, "Entradas aceptadas");
        }

        [TestMethod]
        public void ValidarDatosCorrectosInyeccion() {
            FormularioRevisionModel form;
            form = new FormularioRevisionModel();
            bool resultado = form.ValidarObservaciones("Hola amigos");
            Assert.IsTrue(resultado, "Caracteres invalidos");

        }

        [TestMethod]
        public void ValidarDatosMaliciososInyeccion()
        {
            FormularioRevisionModel form;
            form = new FormularioRevisionModel();
            bool resultado = form.ValidarObservaciones("Hola amigos UPDATE");
            Assert.IsFalse(resultado, "Caracteres invalidos");

        }
    }
}
