using System;
using System.IO;

namespace Commons
{
    public class FileLogger
    {
        public enum LogLugares
        { Log4Net, Archivo, Ambos }

        public enum LogTipos
        { FATAL, ERROR, WARN, INFO, DEBUG, ALL }

        public static LogLugares LogLugaresPorNombre(string logNombre)
        {
            LogLugares resultado;
            switch (logNombre.ToLower())
            {
                case "log":
                    resultado = LogLugares.Log4Net;
                    break;
                case "archivo":
                    resultado = LogLugares.Archivo;
                    break;
                default:
                    resultado = LogLugares.Ambos;
                    break;
            }
            return resultado;
        }

        public static LogTipos LogTiposPorNombre(string logNombre)
        {
            LogTipos resultado = new LogTipos();
            switch (logNombre.ToUpper())
            {
                case "FATAL":
                    resultado = LogTipos.FATAL;
                    break;
                case "ERROR":
                    resultado = LogTipos.ERROR;
                    break;
                case "WARN":
                    resultado = LogTipos.WARN;
                    break;
                case "INFO":
                    resultado = LogTipos.INFO;
                    break;
                case "DEBUG":
                    resultado = LogTipos.DEBUG;
                    break;
                default:
                    resultado = LogTipos.ALL;
                    break;
            }
            return resultado;
        }

        public static void WriteToFile(string Message, LogTipos tipo = LogTipos.INFO, bool RegistraFecha = true, LogLugares logLugares = LogLugares.Ambos, log4net.ILog log = null)
        {
            if (log == null)
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            if (logLugares == LogLugares.Archivo || logLugares == LogLugares.Ambos)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string Message1 = RegistraFecha ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss -> ") + Message : Message;

                string fileName = tipo == LogTipos.INFO ? "ServiceLog_" : "ServiceLog_Error_";
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + fileName + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message1);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message1);
                    }
                }
            }
            if ((logLugares == LogLugares.Log4Net || logLugares == LogLugares.Ambos) && (log != null))
            {
                if (tipo == LogTipos.INFO)
                    log.Info(Message);
                else if (tipo == LogTipos.ERROR)
                    log.Error(Message);
                else if (tipo == LogTipos.FATAL)
                    log.Fatal(Message);
            }
        }
    }
}
