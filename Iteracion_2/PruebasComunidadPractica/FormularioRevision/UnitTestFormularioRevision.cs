using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iteracion_2.Controllers;
using Iteracion_2.Models;


namespace PruebasComunidadPractica.FormularioRevision
{
    [TestClass]
    public class UnitTestFormularioRevision
    {
        
        [TestMethod]
        public void ValidarTodasEntradasUsuario()
        {
            FormularioRevisionController form;
            form = new FormularioRevisionController();
            bool resultado = form.ValidarEntradas("2","2","2");

            Assert.IsTrue(resultado, "Entradas invalidas");

            
        }
        [TestMethod]
        public void ValidarEntradasSinContribucion()
        {
            FormularioRevisionController form;
            form = new FormularioRevisionController();
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
