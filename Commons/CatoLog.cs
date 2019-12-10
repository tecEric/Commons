using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    class CatoLog
    {
        public static EventLog elog = new EventLog();
        
        public static void Registra(string app_Name, string mensaje, EventLogEntryType entryType, int funcion = 0)
        {
            if (!EventLog.SourceExists(app_Name))
                EventLog.CreateEventSource(app_Name, "Application");
            elog.Source = app_Name;
            elog.EnableRaisingEvents = true;
            //el ID para el EventViewer. cada funcion va a llevar el numero haciendo match con el enum Funciones
            int id_funcion = funcion;
            if (id_funcion > 0)
            { elog.WriteEntry(mensaje, entryType, id_funcion); }
            else
            { elog.WriteEntry(mensaje, entryType); }
        }
    }
}
