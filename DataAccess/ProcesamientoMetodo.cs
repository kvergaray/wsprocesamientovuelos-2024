using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSProcesamientoVuelos.Entity;

namespace WSProcesamientoVuelos.DataAccess
{
    public class ProcesamientoMetodo
    {
        MetodoBD ometodo = new MetodoBD();

        public void InsertVuelos()
        {
            List<logsVueloEntity> result = ometodo.GetLogVuelos();
            foreach (var vuelo in result)
            {
                ometodo.InsertVuelo(vuelo);
            }
        }  
    }
}

 