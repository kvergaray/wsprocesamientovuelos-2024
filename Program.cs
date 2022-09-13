using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MensajeriaService
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        //static void Main()
        //{
        //    ServiceBase[] ServicesToRun;
        //    ServicesToRun = new ServiceBase[]
        //    {
        //        new Service1()
        //    };
        //    ServiceBase.Run(ServicesToRun);
        //}

        public static void Main(string[] args)
        {
            if (args.FirstOrDefault()?.ToUpper() == "/CONSOLE")
            {
                RunAsConsole();
            }
            else
            {
                RunAsService();
            }
        }
        private static void RunAsConsole()
        {
            Service1 serv = new Service1();
            serv.StartService();

            Console.WriteLine("Running service as console. Press any key to stop.");
            Console.ReadLine();

            serv.Stop();
        }
        private static void RunAsService()
        {
            /* Warning: Don't load the object graph or
            * initialize anything in here.
            *
            * Initialize everything in TestService.StartService() instead
            */
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
            new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

