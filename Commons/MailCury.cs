using System;
using System.Configuration;
using System.Net.Mail;

namespace Commons
{
    class MailCury
    {
        public static void SendMail_Test(string subject = "", string body = "", bool test = false, bool localConfig = false)
        {
            try
            {
                MailMessage mail = new MailMessage();

                string[] Server_To = ConfigurationManager.AppSettings["Mail.SMTP.Server.To"].ToString().Split(';');
                string Priority = ConfigurationManager.AppSettings["Mail.Priority"].ToString();
                string destinatarios = string.Empty;
                foreach (string s in Server_To)
                {
                    mail.To.Add(s);
                    destinatarios += s + "\r\n";
                }
                FileLogger.WriteToFile(Message: "Correo enviado a: " + destinatarios, tipo: FileLogger.LogTipos.FATAL, RegistraFecha: false, logLugares: FileLogger.LogLugaresPorNombre(ConfigurationManager.AppSettings["Log_Archivo"].ToString()));
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                FileLogger.WriteToFile("Error enviando Correo", FileLogger.LogTipos.FATAL);
                FileLogger.WriteToFile(ex.Message.ToString(), FileLogger.LogTipos.FATAL);
            }
        }

        public static void SendMail(string subject = "", string body = "", bool test = false, bool localConfig = false)
        {
            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["Mail.SMTP.Server.Name"].ToString());
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Mail.SMTP.Server.From"].ToString());
                string[] Server_To = ConfigurationManager.AppSettings["Mail.SMTP.Server.To"].ToString().Split(';');
                string Priority = ConfigurationManager.AppSettings["Mail.Priority"].ToString();
                string destinatarios = string.Empty;
                foreach (string s in Server_To)
                {
                    mail.To.Add(s);
                    destinatarios += s + "\r\n";
                }

                if (ConfigurationManager.AppSettings["Cc"].ToString().Length > 0)
                {
                    Server_To = null;
                    Server_To = ConfigurationManager.AppSettings["Cc"].ToString().Split(';');
                    foreach (string s in Server_To)
                    {
                        mail.CC.Add(s);
                        destinatarios += "Cc: " + s + "\r\n";
                    }
                }

                if (ConfigurationManager.AppSettings["BCc"].ToString().Length > 0)
                {
                    Server_To = null;
                    Server_To = ConfigurationManager.AppSettings["BCc"].ToString().Split(';');
                    foreach (string s in Server_To)
                    {
                        mail.Bcc.Add(s);
                    }
                }
                if (test)
                {
                    mail.Subject = "Test Mail";
                    mail.Body = "This is for testing SMTP mail from Heimdall and MailCury";
                }
                else
                {
                    mail.Subject = subject;
                    mail.Body = body;
                }
                switch (Priority.ToLower())
                {
                    case "high":
                        mail.Priority = System.Net.Mail.MailPriority.High; break;
                    case "low":
                        mail.Priority = System.Net.Mail.MailPriority.Low; break;
                    default:
                        mail.Priority = System.Net.Mail.MailPriority.Normal; break;
                }
                SmtpServer.Send(mail);
                Console.Write("Correo enviado");
                FileLogger.WriteToFile(Message: "Correo enviado a: " + destinatarios, tipo: FileLogger.LogTipos.FATAL, RegistraFecha: false, logLugares: FileLogger.LogLugaresPorNombre(ConfigurationManager.AppSettings["Log_Archivo"].ToString()));
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                FileLogger.WriteToFile("Error enviando Correo", FileLogger.LogTipos.FATAL);
                FileLogger.WriteToFile(ex.Message.ToString(), FileLogger.LogTipos.FATAL);
            }
        }
    }
}
