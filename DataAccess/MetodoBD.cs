using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSProcesamientoVuelos.Entity;

namespace WSProcesamientoVuelos.DataAccess
{
    public class MetodoBD
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string Db = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conexion;
        FileMetodo ometodo = new FileMetodo();

        public bool conectar()
        {
                bool conectado = false;
                conexion = new SqlConnection(Db);
                try
                {
                    conexion.Open();
                    conectado = true;
            }
                catch (SqlException ex)
                {
                    logger.Error("*****Error al conectar a la base de datos*****");
                    logger.Error("Message: " + ex.Message.ToString());
                    logger.Error(ex.ToString());
            }
                return conectado;
        }

        public int InsertLogVuelo(VueloBody oVuelo, string filename, string fullPath, DateTime fch_proceso)
        {
            int result = 0;
            oVuelo.fch_hra_real = oVuelo.fch_hra_real == "" || oVuelo.fch_hra_real == null ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_real;
            oVuelo.fch_hra_est = oVuelo.fch_hra_est == "" || oVuelo.fch_hra_est == null? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_est;
            oVuelo.fch_hra_mostrador_ini = oVuelo.fch_hra_mostrador_ini == "" || oVuelo.fch_hra_mostrador_ini == null ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_mostrador_ini;
            oVuelo.fch_hra_mostrador_fin = oVuelo.fch_hra_mostrador_fin == "" || oVuelo.fch_hra_mostrador_fin == null ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_mostrador_fin;
            oVuelo.fch_hra_est_prc_dest = oVuelo.fch_hra_est_prc_dest == "" || oVuelo.fch_hra_est_prc_dest == null ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_est_prc_dest;
            oVuelo.log_fch_cre = oVuelo.log_fch_cre == "" || oVuelo.log_fch_cre == null ? "1900-01-01 00:00:00.000" : oVuelo.log_fch_cre;
            oVuelo.log_fch_mod = oVuelo.log_fch_mod == "" || oVuelo.log_fch_mod == null ? "1900-01-01 00:00:00.000" : oVuelo.log_fch_mod;

            DateTime fch_hra_real = Convert.ToDateTime(oVuelo.fch_hra_real);
            DateTime fch_hra_est = Convert.ToDateTime(oVuelo.fch_hra_est);
            DateTime fch_hra_mostrador_ini = Convert.ToDateTime(oVuelo.fch_hra_mostrador_ini);
            DateTime fch_hra_mostrador_fin = Convert.ToDateTime(oVuelo.fch_hra_mostrador_fin);
            DateTime fch_hra_est_prc_dest = Convert.ToDateTime(oVuelo.fch_hra_est_prc_dest);
            DateTime log_fch_cre = Convert.ToDateTime(oVuelo.log_fch_cre);
            DateTime log_fch_mod = Convert.ToDateTime(oVuelo.log_fch_mod);

            oVuelo.num_puerta = oVuelo.num_puerta == "" || oVuelo.num_puerta == null ? "SNP" : oVuelo.num_puerta;
            using (conexion)
            {
                try
                {
                    conectar();
                    SqlCommand comando = new SqlCommand("sp_InsertLogVuelo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@tip_registro", oVuelo.tip_registro);
                    comando.Parameters.AddWithValue("@cod_vuelo", oVuelo.cod_vuelo);
                    comando.Parameters.AddWithValue("@cod_aerolinea", oVuelo.cod_aerolinea);
                    comando.Parameters.AddWithValue("@num_vuelo", oVuelo.num_vuelo);
                    comando.Parameters.AddWithValue("@tip_ope", oVuelo.tip_ope);
                    comando.Parameters.AddWithValue("@tip_trafico", oVuelo.tip_trafico);
                    comando.Parameters.AddWithValue("@abr_aerolinea", oVuelo.abr_aerolinea);
                    comando.Parameters.AddWithValue("@cod_prc_dest", oVuelo.cod_prc_dest);
                    comando.Parameters.AddWithValue("@dsc_prc_dest", oVuelo.dsc_prc_dest);
                    comando.Parameters.AddWithValue("@fch_hra_prog", oVuelo.fch_hra_prog);
                    comando.Parameters.AddWithValue("@fch_hra_est", fch_hra_est);
                    comando.Parameters.AddWithValue("@fch_hra_real", fch_hra_real);
                    comando.Parameters.AddWithValue("@Fch_Hra_Ult", oVuelo.fch_hra_ult);
                    comando.Parameters.AddWithValue("@dsc_estado", oVuelo.dsc_estado);
                    comando.Parameters.AddWithValue("@num_term_aeronave", oVuelo.num_term_aeronave);
                    comando.Parameters.AddWithValue("@num_term_pasajero", oVuelo.num_term_pasajero);
                    comando.Parameters.AddWithValue("@num_faja", oVuelo.num_faja);
                    comando.Parameters.AddWithValue("@num_mostrador", oVuelo.num_mostrador);
                    comando.Parameters.AddWithValue("@fch_hra_mostrador_ini ", fch_hra_mostrador_ini);
                    comando.Parameters.AddWithValue("@fch_hra_mostrador_fin", fch_hra_mostrador_fin);
                    comando.Parameters.AddWithValue("@num_puerta", oVuelo.num_puerta);
                    comando.Parameters.AddWithValue("@num_min_duracion", oVuelo.num_min_duracion);
                    comando.Parameters.AddWithValue("@fch_hra_est_prc_dest", fch_hra_est_prc_dest);
                    comando.Parameters.AddWithValue("@log_usr_cre", oVuelo.log_usr_cre);
                    comando.Parameters.AddWithValue("@log_fch_cre", log_fch_cre);
                    comando.Parameters.AddWithValue("@log_usr_mod", oVuelo.log_usr_mod);
                    comando.Parameters.AddWithValue("@log_fch_mod", log_fch_mod);
                    comando.Parameters.AddWithValue("@log_hostname", oVuelo.log_hostname);
                    comando.Parameters.AddWithValue("@name_file", filename);
                    comando.Parameters.AddWithValue("@fch_proceso", fch_proceso);
                    comando.Parameters.AddWithValue("@tip_mq", "TiempoReal");

                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "")
                        {
                            result = Convert.ToInt32(reader[0]);
                        }
                    }
                    conexion.Close();
                    logger.Info("*****Numero de registro: *****" + result);
                    return result;
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    logger.Error("*****Error en InsertLogVuelo()*****");
                    logger.Error("Message: " + ex.Message.ToString());
                    logger.Error(ex.ToString());
                    return result = 0;
                }
                
                
            }
        }

        public List<logsVueloEntity> GetLogVuelos()
        {
            List<logsVueloEntity> olentity = new List<logsVueloEntity>();
            using (conexion)
            {
                try
                {
                    conectar();
                    SqlCommand comando = new SqlCommand("sp_SelectLogsVuelos", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "")
                        {
                            olentity.Add(new logsVueloEntity {
                                ID = Convert.ToInt32(reader[0]),
                            cod_vuelo = Convert.ToString(reader[1]),
                            tip_ope = Convert.ToString(reader[2]),
                            tip_trafico = Convert.ToString(reader[3]),
                            fch_hra_prog = Convert.ToDateTime(reader[4]),
                            dsc_estado = Convert.ToString(reader[5]),
                            IDPuerta = Convert.ToInt32(reader[6]),
                            fch_hra_ult = Convert.ToDateTime(reader[7])
                            });  
                        }
                    }
                    conexion.Close();

                    logger.Info("*****Obtencion del Logs Vuelos*****");
                    logger.Info("Cantidad de vuelos a procesar: " + olentity.Count);
                    return olentity;
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    
                    logger.Error("*****Error en InsertVuelo()*****");
                    logger.Error("Message: " + ex.Message.ToString());
                    logger.Error(ex.ToString());
                    return olentity;
                }
            }
        }
        
        public void InsertVuelo(logsVueloEntity oVuelo)
        {
            DateTime fch_hra_ult = new DateTime();
            //CALCULO DE REDONDEO
            int redondeo;

            int minute = oVuelo.fch_hra_ult.Minute;
            int second = oVuelo.fch_hra_ult.Second;
            

            if (minute <= 30)
            {
                if (minute <= 15)
                {
                    redondeo = minute;
                    fch_hra_ult = oVuelo.fch_hra_ult.AddMinutes(-redondeo);
                }
                else
                {
                    redondeo = 30 - minute;
                    fch_hra_ult = oVuelo.fch_hra_ult.AddMinutes(redondeo);
                }

            }
            else
            {
                if (minute >= 30)
                {
                    if (minute >= 45)
                    {
                        redondeo = 60 - minute;
                        fch_hra_ult=oVuelo.fch_hra_ult.AddMinutes(redondeo);

                    }
                    else
                    {
                        redondeo = minute - 30;
                        fch_hra_ult=oVuelo.fch_hra_ult.AddMinutes(-redondeo);
                    }

                }
            }
            if (second != 0)
            {
                fch_hra_ult = fch_hra_ult.AddSeconds((60 - second));
                fch_hra_ult = fch_hra_ult.AddMinutes(-1);
            }

            //DateTime fch_hra_encendido = oVuelo.fch_hra_ult.AddHours(-2);
            //DateTime fch_hra_apagado = oVuelo.fch_hra_ult.AddMinutes(30);
            
            using (conexion)
            {
                try
                {
                    conectar();
                    SqlCommand comando = new SqlCommand("sp_InsertVuelo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    
                    comando.Parameters.AddWithValue("@cod_vuelo", oVuelo.cod_vuelo);
                    comando.Parameters.AddWithValue("@tip_ope", oVuelo.tip_ope);
                    comando.Parameters.AddWithValue("@tip_trafico", oVuelo.tip_trafico);
                    comando.Parameters.AddWithValue("@fch_hra_prog", oVuelo.fch_hra_prog);
                    comando.Parameters.AddWithValue("@dsc_estado", oVuelo.dsc_estado);
                    comando.Parameters.AddWithValue("@IDPuerta", oVuelo.IDPuerta);
                    comando.Parameters.AddWithValue("@fch_hra_ult", fch_hra_ult);
                    comando.Parameters.AddWithValue("@idLogVuelo", oVuelo.ID);

                    comando.ExecuteNonQuery();
                    logger.Info("*****Inserccion existosa de Vuelo*****");



                    logger.Info("Estado: " + oVuelo.dsc_estado + "  Cod. Vuelo: " + oVuelo.cod_vuelo + "  T. Operacion: " + oVuelo.tip_ope);
                    logger.Info("  T. Trafico: " + oVuelo.tip_trafico + "  F. Programada: " + oVuelo.fch_hra_prog + "  F. Ultima: " + oVuelo.fch_hra_ult);

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    logger.Error("*****Error en InsertVuelo()*****");
                    logger.Error("Message: " + ex.Message.ToString());
                    logger.Error(ex.ToString());
                }
                finally
                {

                }
            }
        }

    }
}
