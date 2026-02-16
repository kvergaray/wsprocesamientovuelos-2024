using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using WSProcesamientoVuelos.Entity;

namespace WSProcesamientoVuelos.DataAccess
{
    public class XmlMetodo
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string xmlPath = System.Configuration.ConfigurationManager.AppSettings["xmlPath"];
        private string xmlFinalPath = System.Configuration.ConfigurationManager.AppSettings["xmlFinalPath"];
        private string xmlName = System.Configuration.ConfigurationManager.AppSettings["xmlName"];

        public int InsertLogVuelo(VueloBody oVuelo, string filename, string fullPath, DateTime fch_proceso)
        {
            int result = 0;

            oVuelo.fch_hra_ult = string.IsNullOrEmpty(oVuelo.fch_hra_ult) ? oVuelo.fch_hra_prog.ToString() : oVuelo.fch_hra_ult;
            oVuelo.fch_hra_real = string.IsNullOrEmpty(oVuelo.fch_hra_real) ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_real;
            oVuelo.fch_hra_est = string.IsNullOrEmpty(oVuelo.fch_hra_est) ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_est;
            oVuelo.fch_hra_mostrador_ini = string.IsNullOrEmpty(oVuelo.fch_hra_mostrador_ini) ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_mostrador_ini;
            oVuelo.fch_hra_mostrador_fin = string.IsNullOrEmpty(oVuelo.fch_hra_mostrador_fin) ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_mostrador_fin;
            oVuelo.fch_hra_est_prc_dest = string.IsNullOrEmpty(oVuelo.fch_hra_est_prc_dest) ? "1900-01-01 00:00:00.000" : oVuelo.fch_hra_est_prc_dest;
            oVuelo.log_fch_cre = string.IsNullOrEmpty(oVuelo.log_fch_cre) ? "1900-01-01 00:00:00.000" : oVuelo.log_fch_cre;
            oVuelo.log_fch_mod = string.IsNullOrEmpty(oVuelo.log_fch_mod) ? "1900-01-01 00:00:00.000" : oVuelo.log_fch_mod;

            oVuelo.Filename = filename;
            oVuelo.FullPath = fullPath;

            if (!Directory.Exists(xmlPath))
            {
                Directory.CreateDirectory(xmlPath);
            }

            string rutaArchivo = Path.Combine(xmlPath, xmlName + ".xml");

            if (!File.Exists(rutaArchivo))
            {
                GuardarObjetoComoXml(oVuelo, rutaArchivo);
                result = 1;
            }
            else
            {
                ActualizarXmlConNuevoVuelo(oVuelo, rutaArchivo);
                result = 1;
            }

            return result;
        }

        public static void GuardarObjetoComoXml(VueloBody vuelo, string fullPath)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("Vuelos");
            doc.AppendChild(root);

            XmlNode vueloNodo = CrearNodoVuelo(doc, vuelo);
            root.AppendChild(vueloNodo);

            doc.Save(fullPath);

            Console.WriteLine("XML creado y vuelo guardado.");
        }

        public static XmlNode CrearNodoVuelo(XmlDocument doc, VueloBody vuelo)
        {
            XmlElement vueloNodo = doc.CreateElement("Vuelo");

            AñadirElemento(doc, vueloNodo, "tip_registro", vuelo.tip_registro);
            AñadirElemento(doc, vueloNodo, "cod_vuelo", vuelo.cod_vuelo);
            AñadirElemento(doc, vueloNodo, "cod_aerolinea", vuelo.cod_aerolinea);
            AñadirElemento(doc, vueloNodo, "num_vuelo", vuelo.num_vuelo);
            AñadirElemento(doc, vueloNodo, "tip_ope", vuelo.tip_ope);
            AñadirElemento(doc, vueloNodo, "tip_trafico", vuelo.tip_trafico);
            AñadirElemento(doc, vueloNodo, "abr_aerolinea", vuelo.abr_aerolinea);
            AñadirElemento(doc, vueloNodo, "cod_prc_dest", vuelo.cod_prc_dest);
            AñadirElemento(doc, vueloNodo, "dsc_prc_dest", vuelo.dsc_prc_dest);
            AñadirElemento(doc, vueloNodo, "fch_hra_prog", Convert.ToString(vuelo.fch_hra_prog));
            AñadirElemento(doc, vueloNodo, "fch_hra_est", vuelo.fch_hra_est);
            AñadirElemento(doc, vueloNodo, "fch_hra_real", vuelo.fch_hra_real);
            AñadirElemento(doc, vueloNodo, "fch_hra_ult", vuelo.fch_hra_ult);
            AñadirElemento(doc, vueloNodo, "dsc_estado", vuelo.dsc_estado);
            AñadirElemento(doc, vueloNodo, "num_term_aeronave", vuelo.num_term_aeronave);
            AñadirElemento(doc, vueloNodo, "num_term_pasajero", vuelo.num_term_pasajero);
            AñadirElemento(doc, vueloNodo, "num_faja", vuelo.num_faja);
            AñadirElemento(doc, vueloNodo, "num_mostrador", vuelo.num_mostrador);
            AñadirElemento(doc, vueloNodo, "fch_hra_mostrador_ini", vuelo.fch_hra_mostrador_ini);
            AñadirElemento(doc, vueloNodo, "fch_hra_mostrador_fin", vuelo.fch_hra_mostrador_fin);
            AñadirElemento(doc, vueloNodo, "num_puerta", vuelo.num_puerta);
            AñadirElemento(doc, vueloNodo, "num_min_duracion", vuelo.num_min_duracion);
            AñadirElemento(doc, vueloNodo, "fch_hra_est_prc_dest", vuelo.fch_hra_est_prc_dest);
            AñadirElemento(doc, vueloNodo, "log_usr_cre", vuelo.log_usr_cre);
            AñadirElemento(doc, vueloNodo, "log_fch_cre", vuelo.log_fch_cre);
            AñadirElemento(doc, vueloNodo, "log_usr_mod", vuelo.log_usr_mod);
            AñadirElemento(doc, vueloNodo, "log_fch_mod", vuelo.log_fch_mod);
            AñadirElemento(doc, vueloNodo, "log_hostname", vuelo.log_hostname);
            AñadirElemento(doc, vueloNodo, "Filename", vuelo.Filename);
            AñadirElemento(doc, vueloNodo, "FullPath", vuelo.FullPath);

            return vueloNodo;
        }


        public static void ActualizarXmlConNuevoVuelo(VueloBody nuevoVuelo, string rutaArchivo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo);

            XmlNode root = doc.DocumentElement;
            XmlNode nuevoNodo = CrearNodoVuelo(doc, nuevoVuelo);
            root.AppendChild(nuevoNodo);

            doc.Save(rutaArchivo);

            Console.WriteLine("XML actualizado con nuevo vuelo.");
        }

        public static void AñadirElemento(XmlDocument doc, XmlNode nodoPadre, string nombreElemento, string valorElemento)
        {
            XmlElement nuevoElemento = doc.CreateElement(nombreElemento);
            nuevoElemento.InnerText = valorElemento;
            nodoPadre.AppendChild(nuevoElemento);
        }

        //PROCESAR LOS LOGS VUELOS

        public List<logsVueloEntity> GetLogVuelos()
        {
            List<logsVueloEntity> olentity = new List<logsVueloEntity>();

            try
            {
                string rutaXml = Path.Combine(xmlPath, xmlName + ".xml");
                // Cargar el archivo XML
                XDocument doc = XDocument.Load(rutaXml);

                // Establecer el rango de fecha para la condición
                DateTime fch_hra24 = DateTime.Now.AddMinutes(1410); // 23 horas 30 minutos desde ahora

                // Filtrar los vuelos con LINQ to XML
                var vuelos = from vuelo in doc.Descendants("Vuelo")
                             where new[] { "PROGRAMADO", "CONFIRMADO", "DEMORADO", "FIN EMBARQ", "CANCELADO" }
                                   .Contains(vuelo.Element("dsc_estado").Value)
                                   && vuelo.Element("vuelo_estado").Value == "EN ESPERA"
                                   && DateTime.Parse(vuelo.Element("fch_hra_ult").Value) <= fch_hra24
                                   && vuelo.Element("tip_mq").Value == "TiempoReal"
                             select new logsVueloEntity
                             {
                                 ID = int.Parse(vuelo.Element("ID").Value),
                                 cod_vuelo = vuelo.Element("cod_vuelo").Value,
                                 tip_ope = vuelo.Element("tip_ope").Value,
                                 tip_trafico = vuelo.Element("tip_trafico").Value,
                                 fch_hra_prog = DateTime.Parse(vuelo.Element("fch_hra_prog").Value),
                                 dsc_estado = vuelo.Element("dsc_estado").Value,
                                 IDPuerta = int.Parse(vuelo.Element("IDPuerta").Value),
                                 fch_hra_ult = DateTime.Parse(vuelo.Element("fch_hra_ult").Value)
                             };

                // Agregar los vuelos filtrados a la lista
                olentity = vuelos.ToList();

                // Log de información
                Console.WriteLine("*****Obtención de Logs de Vuelos*****");
                Console.WriteLine("Cantidad de vuelos a procesar: " + olentity.Count);
            }
            catch (Exception ex)
            {
                // Manejo de errores y logs
                Console.WriteLine("*****Error en GetLogVuelos()*****");
                Console.WriteLine("Message: " + ex.Message.ToString());
                Console.WriteLine(ex.ToString());
            }

            return olentity;
        }

        public static int InsertLogVuelo(LogVueloEntity logVuelo, string rutaXml)
        {
            XDocument doc;

            // Verificar si el archivo ya existe
            if (File.Exists(rutaXml))
            {
                doc = XDocument.Load(rutaXml);
            }
            else
            {
                doc = new XDocument(new XElement("LogVuelos"));
            }

            // Insertar el nuevo log de vuelo
            XElement nuevoLog = new XElement("LogVuelo",
                new XElement("tip_registro", logVuelo.tip_registro),
                new XElement("cod_vuelo", logVuelo.cod_vuelo),
                new XElement("cod_aerolinea", logVuelo.cod_aerolinea),
                new XElement("num_vuelo", logVuelo.num_vuelo),
                new XElement("tip_ope", logVuelo.tip_ope),
                new XElement("tip_trafico", logVuelo.tip_trafico),
                new XElement("abr_aerolinea", logVuelo.abr_aerolinea),
                new XElement("cod_prc_dest", logVuelo.cod_prc_dest),
                new XElement("dsc_prc_dest", logVuelo.dsc_prc_dest),
                new XElement("fch_hra_prog", logVuelo.fch_hra_prog),
                new XElement("fch_hra_est", logVuelo.fch_hra_est),
                new XElement("fch_hra_real", logVuelo.fch_hra_real),
                new XElement("fch_hra_ult", logVuelo.fch_hra_ult),
                new XElement("dsc_estado", logVuelo.dsc_estado),
                new XElement("num_term_aeronave", logVuelo.num_term_aeronave),
                new XElement("num_term_pasajero", logVuelo.num_term_pasajero),
                new XElement("num_faja", logVuelo.num_faja),
                new XElement("num_mostrador", logVuelo.num_mostrador),
                new XElement("fch_hra_mostrador_ini", logVuelo.fch_hra_mostrador_ini),
                new XElement("fch_hra_mostrador_fin", logVuelo.fch_hra_mostrador_fin),
                new XElement("num_puerta", logVuelo.num_puerta),
                new XElement("num_min_duracion", logVuelo.num_min_duracion),
                new XElement("fch_hra_est_prc_dest", logVuelo.fch_hra_est_prc_dest),
                new XElement("log_usr_cre", logVuelo.log_usr_cre),
                new XElement("log_fch_cre", logVuelo.log_fch_cre),
                new XElement("log_usr_mod", logVuelo.log_usr_mod),
                new XElement("log_fch_mod", logVuelo.log_fch_mod),
                new XElement("log_hostname", logVuelo.log_hostname),
                new XElement("name_file", logVuelo.name_file),
                new XElement("fch_proceso", logVuelo.fch_proceso),
                new XElement("tip_mq", logVuelo.tip_mq),
                new XElement("vuelo_estado", "EN ESPERA") // Estado inicial como en la lógica SQL
            );

            // Agregar el log al documento
            doc.Root.Add(nuevoLog);

            // Guardar los cambios en el archivo XML
            doc.Save(rutaXml);

            // Devuelve un ID simulado (aquí puedes generar un ID único si es necesario)
            return doc.Root.Elements("LogVuelo").Count();
        }

        // Método para insertar o actualizar un vuelo en el archivo XML
        public static void InsertVuelo(VueloEntity vuelo, string rutaXml)
        {
            XDocument doc;

            // Verificar si el archivo ya existe
            if (File.Exists(rutaXml))
            {
                doc = XDocument.Load(rutaXml);
            }
            else
            {
                doc = new XDocument(new XElement("Vuelos"));
            }

            // Verificar si el vuelo ya existe en el archivo XML
            var vueloExistente = doc.Descendants("Vuelo")
                .FirstOrDefault(x =>
                    x.Element("cod_vuelo").Value == vuelo.cod_vuelo &&
                    x.Element("tip_ope").Value == vuelo.tip_ope &&
                    x.Element("tip_trafico").Value == vuelo.tip_trafico &&
                    DateTime.Parse(x.Element("fch_hra_prog").Value) == vuelo.fch_hra_prog
                );

            DateTime fch_hra24 = DateTime.Now.AddMinutes(1410); // 23 hrs 30 mins desde la hora actual

            if (vuelo.fch_hra_ult >= DateTime.Now && vuelo.fch_hra_ult <= fch_hra24)
            {
                if (vueloExistente != null) // Vuelo ya existe, actualización
                {
                    DateTime fch_hra_ultExistente = DateTime.Parse(vueloExistente.Element("fch_hra_ult").Value);

                    // Actualizar los datos si hay cambios
                    if (vuelo.fch_hra_ult != fch_hra_ultExistente)
                    {
                        vueloExistente.Element("dsc_estado").Value = vuelo.dsc_estado;
                        vueloExistente.Element("fch_hra_ult").Value = vuelo.fch_hra_ult.ToString();
                        vueloExistente.Element("IDPuerta").Value = vuelo.IDPuerta.ToString();
                        vueloExistente.Element("estado_ejec").Value = "NO EJECUTADO";

                        Console.WriteLine("Vuelo actualizado correctamente en el XML.");
                    }
                }
                else // No existe, inserción
                {
                    // Crear un nuevo elemento <Vuelo> y agregarlo al archivo XML
                    XElement vueloElement = new XElement("Vuelo",
                        new XElement("cod_vuelo", vuelo.cod_vuelo),
                        new XElement("tip_ope", vuelo.tip_ope),
                        new XElement("tip_trafico", vuelo.tip_trafico),
                        new XElement("dsc_estado", vuelo.dsc_estado),
                        new XElement("IDPuerta", vuelo.IDPuerta),
                        new XElement("fch_hra_prog", vuelo.fch_hra_prog),
                        new XElement("fch_hra_ult", vuelo.fch_hra_ult),
                        new XElement("estado_ejec", "NO EJECUTADO")
                    );

                    doc.Root.Add(vueloElement);

                    Console.WriteLine("Vuelo insertado correctamente en el XML.");
                }

                doc.Save(rutaXml); // Guardar el archivo XML
            }
            else if (vuelo.fch_hra_ult > fch_hra24)
            {
                // Vuelo en espera
                Console.WriteLine($"Vuelo {vuelo.cod_vuelo} está 'EN ESPERA'.");
            }
            else if (vuelo.fch_hra_ult <= DateTime.Now)
            {
                // Vuelo vencido
                Console.WriteLine($"Vuelo {vuelo.cod_vuelo} está 'VENCIDO'.");
            }
        }


        //public void InsertVueloXML(string rutaArchivoDestino, List<logsVueloEntity> vuelos)
        //{
        //    try
        //    {
        //        string rutaXml = Path.Combine(xmlFinalPath, xmlName + ".xml");

        //        XDocument doc;

        //        if (!File.Exists(rutaXml))
        //        {
        //            GuardarObjetoComoXml(vuelos, rutaXml);
        //        }

        //        Console.WriteLine("*****Vuelos insertados en el archivo XML correctamente.*****");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("*****Error al insertar vuelos en el archivo XML*****");
        //        Console.WriteLine("Message: " + ex.Message.ToString());
        //        Console.WriteLine(ex.ToString());
        //    }
        //}


    }
}