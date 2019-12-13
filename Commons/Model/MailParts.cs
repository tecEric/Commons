using System;

namespace Commons.Model
{
    public class MailParts
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        private string[] Destinatarios { get; set; }
        public string Destinatarios_lista
        {
            get { return string.Join(";", Destinatarios ?? new string[0]); }
            set
            {
                try
                {
                    Destinatarios = value.ToString().Split(';');
                }
                catch (Exception Dest_ex)
                {
                    Destinatarios = new string[0];
                    FileLogger.WriteToFile(Message: "Error en MailParts.Destinatarios_lista.set\r\n" + Dest_ex.Message, tipo: FileLogger.LogTipos.ERROR);
                    throw Dest_ex;
                }
            }
        }
        public string[] g_Destinatarios
        {
            get
            { return Destinatarios; }
        }

        private string[] Cc { get; set; }
        public string Cc_lista
        {
            get { return string.Join(";", Cc ?? new string[0]); }
            set
            {
                try
                {
                    Cc = value.ToString().Split(';');
                }
                catch (Exception Cc_ex)
                {
                    Cc = new string[0];
                    FileLogger.WriteToFile(Message: "Error en MailParts.Cc_lista.set\r\n" + Cc_ex.Message, tipo: FileLogger.LogTipos.ERROR);
                    throw Cc_ex;
                }
            }
        }
        public string[] g_Cc
        {
            get
            { return Cc; }
        }

        private string[] BCc { get; set; }
        public string BCc_lista
        {
            get { return string.Join(";", BCc ?? new string[0]); }
            set
            {
                try
                {
                    BCc = value.ToString().Split(';');
                }
                catch (Exception BCc_ex)
                {
                    BCc = new string[0];
                    FileLogger.WriteToFile(Message: "Error en MailParts.BCc_lista.set\r\n" + BCc_ex.Message, tipo: FileLogger.LogTipos.ERROR);
                    throw BCc_ex;
                }
            }
        }
        public string[] g_BCc
        {
            get
            { return BCc; }
        }

        public System.Net.Mail.MailPriority Priority { get; set; }

        private string[] Attachments { get; set; }
        public string Attachments_lista
        {
            get { return string.Join(";", Attachments ?? new string[0]); }
            set
            {
                try
                {
                    Attachments = value.ToString().Split(';');
                }
                catch (Exception Attach_ex)
                {
                    Attachments = new string[0];
                    FileLogger.WriteToFile(Message: "Error en MailParts.Attachments_lista.set\r\n" + Attach_ex.Message, tipo: FileLogger.LogTipos.ERROR);
                    throw Attach_ex;
                }
            }
        }
        public string[] g_Attachments
        {
            get
            { return Attachments; }
        }

        public void PrepararNuevo(bool mantenerFrom)
        {
            if (!mantenerFrom)
            { From = string.Empty; }
            Subject = Body = Destinatarios_lista = Cc_lista = BCc_lista = Attachments_lista = string.Empty;
            Priority = System.Net.Mail.MailPriority.Normal;
        }
    }
}
