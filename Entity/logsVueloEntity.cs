using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProcesamientoVuelos.Entity
{

    public class VueloEntity
    {
        public string cod_vuelo { get; set; }
        public string tip_ope { get; set; }
        public string tip_trafico { get; set; }
        public string dsc_estado { get; set; }
        public int IDPuerta { get; set; }
        public DateTime fch_hra_prog { get; set; }
        public DateTime fch_hra_ult { get; set; }
        public int idLogVuelo { get; set; }
        public string estado_ejec { get; set; }
    }

    public class LogVueloEntity
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
        public DateTime fch_hra_est { get; set; }
        public DateTime fch_hra_real { get; set; }
        public DateTime fch_hra_ult { get; set; }
        public string dsc_estado { get; set; }
        public string num_term_aeronave { get; set; }
        public string num_term_pasajero { get; set; }
        public string num_faja { get; set; }
        public string num_mostrador { get; set; }
        public DateTime fch_hra_mostrador_ini { get; set; }
        public DateTime fch_hra_mostrador_fin { get; set; }
        public string num_puerta { get; set; }
        public int num_min_duracion { get; set; }
        public DateTime fch_hra_est_prc_dest { get; set; }
        public string log_usr_cre { get; set; }
        public string log_fch_cre { get; set; }
        public string log_usr_mod { get; set; }
        public string log_fch_mod { get; set; }
        public string log_hostname { get; set; }
        public string name_file { get; set; }
        public DateTime fch_proceso { get; set; }
        public string tip_mq { get; set; }
    }




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

    public class LogVueloBodyXml
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
        public string fch_hra_prog { get; set; }
        public string fch_hra_est { get; set; }
        public string fch_hra_real { get; set; }
        public string fch_hra_ult { get; set; }
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

}
