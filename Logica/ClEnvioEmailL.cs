using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using static Seguridad_JSC.Entidades.ClEnvioEmailE;

namespace Seguridad_JSC.Logica
{
	public class ClEnvioEmailL
	{
        public class EmailService
        {
            private string smtpServer;
            private int smtpPort;
            private string smtpUser;
            private string smtpPassword;

            public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPassword)
            {
                this.smtpServer = smtpServer;
                this.smtpPort = smtpPort;
                this.smtpUser = smtpUser;
                this.smtpPassword = smtpPassword;
            }
            public bool SendEmail(Email email)
            {
                try
                {
                    using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                    {
                        client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                        client.EnableSsl = true;

                        MailMessage mailMessage = new MailMessage
                        {
                            From = new MailAddress(smtpUser),
                            Subject = email.Subject,
                            Body = email.Body,
                            IsBodyHtml = email.IsHtml
                        };
                        mailMessage.To.Add(email.To);

                        client.Send(mailMessage);
                    }

                    Console.WriteLine("Correo enviado correctamente");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                    return false;
                }
            }

        }
    }
}