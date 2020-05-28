using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class MeritoController
    {
        //tenemos que llamar a los modelos 
        MeritoModel meritoModel { set; get; }


        //Agarro lo que me manda la vista y lo paso al modelo
        //Despues tengo que mandarle ese valor a la vista
        public string revisarEstadoArticulo(int artID)
        {
            string valor_a_vista = "";
            //Lamamos para que se cree la conexion
            meritoModel = new MeritoModel();
            meritoModel.connection();
            valor_a_vista = meritoModel.revisarEstadoArticulo(artID);
            return valor_a_vista;
        }


        public string asignarPuntajeInicial(int artID)
        {
            string idAutor_a_vista = "";
            //conexion = new ConexionBD();
            //conexion.connection();
            //conexion.asignarMeritoPuntuacionInicial(artID);

            return idAutor_a_vista;
        }


        public void DegradarMiembros( string nombreUsuario)
        {
            
            meritoModel = new MeritoModel();
            meritoModel.DegradarPeso(nombreUsuario);

            
        }


    }
}