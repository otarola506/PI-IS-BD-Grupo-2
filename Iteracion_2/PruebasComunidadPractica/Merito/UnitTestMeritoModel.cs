using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace PruebasComunidadPractica.Merito
{
    [TestClass]
    public class UnitTestMeritoModel
    {
        [TestMethod]
        public void ObtenerPesoMiembroTest()
        {
            var mockMeritoMiembro = new Mock<IMeritoModel>();
            mockMeritoMiembro.Setup(x => x.ObtenerPeso("Coordinador")).Returns(5);

            var meritoMiembro = new MeritoModelT(mockMeritoMiembro.Object);

            Assert.AreEqual(5, meritoMiembro.ObtenerPeso("Coordinador"));
        }

        [TestMethod]
        public void DegradarPesoMiembroTestQueNoSeaMinimo()
        {
            var mockMeritoMiembro = new Mock<IMeritoModel>();
            mockMeritoMiembro.Setup(x => x.DegradarPeso("otarola506")).Returns("La operación fue realizada con éxito");
            var meritoMiembro = new MeritoModelT(mockMeritoMiembro.Object);
            Assert.AreEqual("La operación fue realizada con éxito", meritoMiembro.DegradarPeso("otarola506"));


        }

        [TestMethod]
        public void DegradarPesoMiembroTestQueSeaMinimo()
        {
            var mockMeritoMiembro = new Mock<IMeritoModel>();
            mockMeritoMiembro.Setup(x => x.DegradarPeso("pepitin")).Returns("Este miembro tiene el peso mas bajo, no se puede bajar mas.");
            var meritoMiembro = new MeritoModelT(mockMeritoMiembro.Object);
            Assert.AreEqual("Este miembro tiene el peso mas bajo, no se puede bajar mas.", meritoMiembro.DegradarPeso("pepitin"));


        }

    }
}
