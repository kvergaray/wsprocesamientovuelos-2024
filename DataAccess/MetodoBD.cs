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
                    comando.Parameters.AddWithValue("@fch_hra_est", oVuelo.fch_hra_est);
                    comando.Parameters.AddWithValue("@fch_hra_real", oVuelo.fch_hra_real);
                    comando.Parameters.AddWithValue("@Fch_Hra_Ult", oVuelo.fch_hra_ult);
                    comando.Parameters.AddWithValue("@dsc_estado", oVuelo.dsc_estado);
                    comando.Parameters.AddWithValue("@num_term_aeronave", oVuelo.num_term_aeronave);
                    comando.Parameters.AddWithValue("@num_term_pasajero", oVuelo.num_term_pasajero);
                    comando.Parameters.AddWithValue("@num_faja", oVuelo.num_faja);
                    comando.Parameters.AddWithValue("@num_mostrador", oVuelo.num_mostrador);
                    comando.Parameters.AddWithValue("@fch_hra_mostrador_ini ", oVuelo.fch_hra_mostrador_ini);
                    comando.Parameters.AddWithValue("@fch_hra_mostrador_fin", oVuelo.fch_hra_mostrador_fin);
                    comando.Parameters.AddWithValue("@num_puerta", oVuelo.num_puerta);
                    comando.Parameters.AddWithValue("@num_min_duracion", oVuelo.num_min_duracion);
                    comando.Parameters.AddWithValue("@fch_hra_est_prc_dest", oVuelo.fch_hra_est_prc_dest);
                    comando.Parameters.AddWithValue("@log_usr_cre", oVuelo.log_usr_cre);
                    comando.Parameters.AddWithValue("@log_fch_cre", oVuelo.log_fch_cre);
                    comando.Parameters.AddWithValue("@log_usr_mod", oVuelo.log_usr_mod);
                    comando.Parameters.AddWithValue("@log_fch_mod", oVuelo.log_fch_mod);
                    comando.Parameters.AddWithValue("@log_hostname", oVuelo.log_hostname);
                    comando.Parameters.AddWithValue("@name_file", filename);
                    comando.Parameters.AddWithValue("@fch_proceso", fch_proceso);
                    
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "")
                        {
                            result = Convert.ToInt32(reader[0]);
                        }
                    }
                    conexion.Close();

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
        public void InsertVuelo(VueloBody oVuelo)
        {
            using (conexion)
            {
                try
                {
                    conectar();
                    SqlCommand comando = new SqlCommand("sp_InsertVuelo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    DateTime fch_hra_encendido = oVuelo.fch_hra_ult.AddMinutes(-120);
                    DateTime fch_hra_apagado = oVuelo.fch_hra_ult.AddMinutes(30);

                    comando.Parameters.AddWithValue("@cod_vuelo", oVuelo.cod_vuelo);
                    comando.Parameters.AddWithValue("@tip_ope", oVuelo.tip_ope);
                    comando.Parameters.AddWithValue("@tip_trafico", oVuelo.tip_trafico);
                    comando.Parameters.AddWithValue("@fch_hra_prog", oVuelo.fch_hra_prog);
                    comando.Parameters.AddWithValue("@dsc_estado", oVuelo.dsc_estado);
                    comando.Parameters.AddWithValue("@num_puerta", oVuelo.num_puerta);
                    comando.Parameters.AddWithValue("@fch_hra_ult", oVuelo.fch_hra_ult);
                    comando.Parameters.AddWithValue("@fch_hra_encendido", fch_hra_encendido);
                    comando.Parameters.AddWithValue("@fch_hra_apagado", fch_hra_apagado);
                   
                    comando.ExecuteNonQuery();
                    logger.Info("*****Inserccion existosa de Vuelo*****");
                    logger.Info("Estado: " + oVuelo.dsc_estado + "  Cod. Vuelo: " + oVuelo.cod_vuelo + "  T. Operacion: " + oVuelo.tip_ope);
                    logger.Info("  T. Trafico: " + oVuelo.tip_trafico + "  F. Programada: " + oVuelo.fch_hra_prog);

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
