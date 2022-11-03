using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;
using WSProcesamientoVuelos.Entity;

namespace WSProcesamientoVuelos.DataAccess
{
    public class FileMetodo
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private string path = System.Configuration.ConfigurationManager.AppSettings["path"];
        private string detinationPath = System.Configuration.ConfigurationManager.AppSettings["detinationPath"];
        

        public void ObtenerPendientes() 
        {
           
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles("*.txt");
            
                foreach (FileInfo file in files)
                {
                    logger.Info(" ");
                    logger.Info("*****Inicio de proceso de log:  " + file.Name + "*****");
                    serializarDocumento(file.FullName, file.Name);
                    logger.Info("*****Fin de proceso de log:  " + file.Name + "*****");
                    logger.Info(" ");
                }
            }
            catch (Exception e)
            {
                logger.Error("*****Error al obtener pendientes*****");
                logger.Error("Message: " + e.Message.ToString());
                logger.Error(e.ToString());
            }
        }

        public void serializarDocumento(string fullPath, string filename)
        {
            StreamWriter stream = null;
            try
            {
                string readTextold = File.ReadAllText(fullPath);
                string readText = readTextold.Trim();
                var doc = new XmlDocument();
                doc.LoadXml(readText);

                var json = JsonConvert.SerializeXmlNode(doc);
                var des = JsonConvert.DeserializeObject<LogInfo>(json);

                var vjson = JsonConvert.DeserializeObject<VueloResponse>(des.Log.Message.Body.Message);

                
                foreach (var vuelo in vjson.Vuelos)
                {
                    var dt = DateTime.Now;
                    MetodoBD ometodo = new MetodoBD();
                    int result = ometodo.InsertLogVuelo(vuelo, filename, fullPath, dt);
                    if (result != 0)
                    {
                        MoverArchivoProcesado(fullPath, filename);

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("*****Error al serializar file*****");
                logger.Error("Message: " + ex.Message.ToString());
                logger.Error(ex.ToString());
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        public void MoverArchivoProcesado(string sourceFile, string filename)
        {
            try
            {
                string destinationFile = detinationPath + filename;
                System.IO.File.Move(sourceFile, destinationFile);
                logger.Info("*****Mover archivo a carperta Procesados/*****");

            }
            catch (Exception e)
            {
                logger.Error("*****Error al mover archivo de pendientes/ a procesados/*****");
                logger.Error("Message: " + e.Message.ToString());
                logger.Error(e.ToString());
            }
        }

    }
}
