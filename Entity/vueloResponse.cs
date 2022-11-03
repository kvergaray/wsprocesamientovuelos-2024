using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProcesamientoVuelos.Entity
{
    public class VueloResponse
    {
        public List<VueloBody> Vuelos { get; set; }
        public VueloBody Item { get; set; }
    }

    public class VueloBody
    {
        public string tip_registro { get; set; }
        public string cod_vuelo { get; set; }
        public string cod_aerolinea { get; set; }
        public string num_vuelo { get; set; }
        public string tip_ope { get; set; }
        public string tip_trafico { get; set; }
        public string abr_aerolinea { get; set; }
        public string cod_prc_dest { get; set; }
        public string dsc_prc_dest { get; set; }
        public DateTime fch_hra_prog { get; set; }
        public string fch_hra_est { get; set; } 
        public string fch_hra_real { get; set; }
        public DateTime fch_hra_ult { get; set; }
        public string dsc_estado { get; set; }
        public string num_term_aeronave { get; set; }
        public string num_term_pasajero { get; set; }
        public string num_faja { get; set; }
        public string num_mostrador { get; set; }
        public string fch_hra_mostrador_ini { get; set; }
        public string fch_hra_mostrador_fin { get; set; }
        public string num_puerta { get; set; }
        public string num_min_duracion { get; set; }
        public string fch_hra_est_prc_dest { get; set; }
        public string log_usr_cre { get; set; }
        public string log_fch_cre { get; set; }
        public string log_usr_mod { get; set; }
        public string log_fch_mod { get; set; }
        public string log_hostname { get; set; }
    }

    public class Info
    {
        public InfoBody Message { get; set; }
    }

    public class LogInfo
    {
        public Info Log { get; set; }
    }

    public class InfoBody
    {
        public MessageBody Body { get; set; }

        public VueloResponse Vuelos { get; set; }
    }

    public class MessageBody
    {

        public string Message { get; set; }
    }
}

