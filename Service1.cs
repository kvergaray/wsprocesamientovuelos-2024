using System;
using System.ServiceProcess;
using System.Configuration;
using System.Threading;
using System.Reflection;
using log4net;
using WSProcesamientoVuelos.DataAccess;

namespace MensajeriaService
{
    public partial class Service1 : ServiceBase
    {
        public static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            new Thread(StartService).Start();
        }
        internal void StartService()
        {
            /*
            This is the true composition root for a service,
            so initialize everything in here
            */
            Console.WriteLine("Starting service");
            //logger.Info("Servicio Iniciado");
            this.ScheduleService();
            this.ScheduleServiceClean();
        }

        protected override void OnStop()
        {
            logger.Info("WindowsService stopped ");
            this.Schedular.Dispose();
            this.SchedularClean.Dispose();
        }

        public void Working()
        {
            FileMetodo ofmetodo = new FileMetodo();
            ProcesamientoMetodo opmetodo = new ProcesamientoMetodo();

            ofmetodo.ObtenerPendientes();

            opmetodo.InsertVuelos();
        }

        private Timer Schedular;

        public void ScheduleService()
        {
            try
            {

                Schedular = new Timer(new TimerCallback(SchedularCallback));
                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                //logger.Info("WindowsService Mode: " + mode + " ");

                //Set the Default Time.
                DateTime scheduledTime = DateTime.MinValue;

                if (mode == "DAILY")
                {
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == "INTERVAL")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                if (mode.ToUpper() == "INTERVALSECONDS")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalSeconds"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddSeconds(intervalSeconds);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddSeconds(intervalSeconds);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format(" day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                logger.Info("WindowsService programado para ejecutarse después de: " + schedule + " ");

                //Get the difference in Minutes between the Scheduled and Current Time.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                Schedular.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                logger.Info("WindowsService Error on:  " + ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("Continental.WindowsService"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallback(object e)
        {
            logger.Info("WindowsService: Entro a ejecutar método");
            this.Working();
            this.ScheduleService();
        }


       //SERVICIO DE LIMPIEZA

        private Timer SchedularClean;

        public void ScheduleServiceClean()
        {
            try
            {

                SchedularClean = new Timer(new TimerCallback(SchedularCallbackClean));
                string mode = ConfigurationManager.AppSettings["Mode.Clean"].ToUpper();
                //logger.Info("WindowsService Mode: " + mode + " ");

                //Set the Default Time.
                DateTime scheduledTime = DateTime.MinValue;
                
                if (mode.ToUpper() == "INTERVAL")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes.Clean"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                if (mode.ToUpper() == "INTERVALSECONDS")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalSeconds.Clean"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddSeconds(intervalSeconds);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddSeconds(intervalSeconds);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format(" day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                logger.Info("La limpieza ciclica esta programado para ejecutarse en: " + schedule + " " + DateTime.Now.AddMinutes(40));

                //Get the difference in Minutes between the Scheduled and Current Time.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                SchedularClean.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                logger.Info("WindowsService Error on:  " + ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("Continental.WindowsService"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallbackClean(object e)
        {
            logger.Info("WindowsService: Entro a ejecutar método");
            CiclicoMetodo ociclico = new CiclicoMetodo();
            ociclico.ciclico24hrs();
            this.ScheduleServiceClean();
        }
    }
}
