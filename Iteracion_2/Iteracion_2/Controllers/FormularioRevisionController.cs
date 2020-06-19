using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iteracion_2.Models;

namespace Iteracion_2.Controllers
{
    public class FormularioRevisionController
    {

        FormularioRevisionModel FormularioMod { get; set; }

        public void ProcesarFormulario(int opinion, int contribucion, int forma, string observaciones, string miembroID, string artID)
        {
            FormularioMod = new FormularioRevisionModel();
            FormularioMod.ProcesarFormulario(opinion, contribucion, forma, observaciones, miembroID, artID);

        }


    }
}
