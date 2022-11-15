using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProcesamientoVuelos.DataAccess
{
    public class CiclicoMetodo
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

        public void ciclico24hrs()
        {
            List<string> olresult = new List<string>();
            try
                 {
                    conectar();
                    SqlCommand comando = new SqlCommand("sp_Ciclico24hrs", conexion);
                     comando.CommandType = CommandType.StoredProcedure;
                //comando.ExecuteNonQuery();
                //conexion.Close();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "")
                        {
                            olresult.Add(Convert.ToString(reader[0]));
                        }
                    }
                    conexion.Close();
                    logger.Info("***** Ejecutando la limpieza de Horario antes de : "+ DateTime.Now.ToString() + " *****");
                    logger.Info("Cantidad de horarios limpiados ejecutados: " + olresult.Count);
                    logger.Info("Detalles de la limpieza: ");
                    foreach (string r in olresult)
                    {
                        logger.Info("- " + r);
                    }
            }
                 catch (Exception ex)
                 {
                    conexion.Close();

                     logger.Error("*****Error a limpiar Horario()*****");
                     logger.Error("Message: " + ex.Message.ToString());
                     logger.Error(ex.ToString());
                    
                 }
             
        }
    }
}
