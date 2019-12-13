using Commons.Model;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace Commons
{
    public class MailCury
    {
        private MailMessage Mail;
        private SmtpClient SmtpServer;
        public MailParts MailSettings { get; set; }

        #region Construct
        public MailCury(string smtpServer)
        {
            Init(smtpServer);
        }

        public MailCury(string smtpServer, MailParts mailSettings)
        {
            Init(smtpServer, mailSettings);
        }

        public MailCury()
        {
            Init();
        }
        #endregion

        #region Init
        private void Init()
        {
            Mail = new MailMessage();
            SmtpServer = SmtpServer ?? new SmtpClient();
            MailSettings = MailSettings ?? new MailParts();
        }

        private void Init(string smtpServer)
        {
            Init();
            SmtpServer = new SmtpClient(smtpServer);
        }

        private void Init(string smtpServer, MailParts mailSettings)
        {
            Init(smtpServer);
            MailSettings = mailSettings;
            CargaMailSettings();
        }

        private void Init(MailParts mailSettings)
        {
            Init();
            MailSettings = mailSettings;
            CargaMailSettings();
        }
        #endregion

        public void CargaMailSettings()
        {
            Mail.From = new MailAddress(MailSettings.From);
            if (!string.IsNullOrEmpty(MailSettings.Destinatarios_lista))
            {
                Mail.To.Add(MailSettings.Destinatarios_lista);
            }
            if (!string.IsNullOrEmpty(MailSettings.Cc_lista))
            {
                Mail.CC.Add(MailSettings.Cc_lista);
            }
            if (!string.IsNullOrEmpty(MailSettings.BCc_lista))
            {
                Mail.Bcc.Add(MailSettings.BCc_lista);
            }
            if (MailSettings.g_Attachments != null)
            {
                foreach (string a in MailSettings.g_Attachments.Where(c => c.Length > 0))
                {
                    Mail.Attachments.Add(new Attachment(a));
                }
            }
            Mail.Priority = MailSettings.Priority;
            Mail.Body = MailSettings.Body ?? string.Empty;
            Mail.Subject = MailSettings.Subject ?? string.Empty;
        }

        public void CargaMailSettings(MailParts mailSettings)
        {
            MailSettings = mailSettings;
            CargaMailSettings();
        }

        public bool EnviarCorreo()
        {
            try
            {
                CargaMailSettings();
                SmtpServer.Send(Mail);
                return true;
            }
            catch (Exception EnvEx)
            {
                Console.Write(EnvEx.ToString());
                FileLogger.WriteToFile(Message: "Error enviando Correo", tipo: FileLogger.LogTipos.ERROR);
                FileLogger.WriteToFile(Message: EnvEx.Message.ToString(), tipo: FileLogger.LogTipos.ERROR);
                return false;
                throw EnvEx;
            }
        }

        public bool EnviarCorreo(string subject, string body)
        {
            try
            {
                MailSettings.Subject = subject;
                MailSettings.Body = body;
                EnviarCorreo();
                return true;
            }
            catch (Exception EnvEx)
            {
                Console.Write(EnvEx.ToString());
                FileLogger.WriteToFile(Message: "Error enviando Correo (2)", tipo: FileLogger.LogTipos.ERROR);
                FileLogger.WriteToFile(Message: EnvEx.Message.ToString(), tipo: FileLogger.LogTipos.ERROR);
                return false;
                throw EnvEx;
            }
        }

        public void PrepararNuevo(bool mantenerFrom)
        {
            string oldFrom = mantenerFrom ? Mail.From.Address : string.Empty;
            MailSettings.PrepararNuevo(mantenerFrom);
            Init();
            if (mantenerFrom)
                Mail.From = new MailAddress(oldFrom);
        }
    }
}
