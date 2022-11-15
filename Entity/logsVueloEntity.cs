using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProcesamientoVuelos.Entity
{
    public class logsVueloEntity
    {
        public int ID { get; set; }
        public string cod_vuelo { get; set; }
        public string tip_ope { get; set; }
        public string tip_trafico { get; set; }
        public DateTime fch_hra_prog { get; set; }
        public DateTime fch_hra_ult { get; set; }
        public string dsc_estado { get; set; }
        public int IDPuerta { get; set; }
    }
}
