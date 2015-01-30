using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace UBCTextbooksCore.Custom
{
    public class Email
    {
        public static void SendVerificationEmail(string toAddress, string subject, string body)
        {
            /*MailMessage mail = new MailMessage();

            mail.From = new MailAddress(ConfigurationSettings.AppSettings[SiteMailAddress]);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mail);*/
            try
            {
                GmailMessage.SendFromGmail("ubctextbooksca", "ww610ww715", toAddress, subject, body);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
        }

        private const string SiteMailAddress = "SiteMailAddress";
    }

    public class GmailMessage : System.Web.Mail.MailMessage
    {

        #region CDO Configuration Constants

        private const string SMTP_SERVER = "http://schemas.microsoft.com/cdo/configuration/smtpserver";
        private const string SMTP_SERVER_PORT = "http://schemas.microsoft.com/cdo/configuration/smtpserverport";
        private const string SEND_USING = "http://schemas.microsoft.com/cdo/configuration/sendusing";
        private const string SMTP_USE_SSL = "http://schemas.microsoft.com/cdo/configuration/smtpusessl";
        private const string SMTP_AUTHENTICATE = "http://schemas.microsoft.com/cdo/configuration/smtpauthenticate";
        private const string SEND_USERNAME = "http://schemas.microsoft.com/cdo/configuration/sendusername";
        private const string SEND_PASSWORD = "http://schemas.microsoft.com/cdo/configuration/sendpassword";

        #endregion

        #region Private Variables

        private static string _gmailServer = "smtp.gmail.com";
        private static long _gmailPort = 465;
        private string _gmailUserName = string.Empty;
        private string _gmailPassword = string.Empty;

        #endregion

        #region Public Members

        /// <summary>
        /// Constructor, creates the GmailMessage object
        /// </summary>
        /// <param name="gmailUserName">The username of the gmail account that the message will be sent through</param>
        /// <param name="gmailPassword">The password of the gmail account that the message will be sent through</param>
        public GmailMessage(string gmailUserName, string gmailPassword)
        {
            this.Fields[SMTP_SERVER] = GmailMessage.GmailServer;
            this.Fields[SMTP_SERVER_PORT] = GmailMessage.GmailServerPort;
            this.Fields[SEND_USING] = 2;
            this.Fields[SMTP_USE_SSL] = true;
            this.Fields[SMTP_AUTHENTICATE] = 1;
            this.Fields[SEND_USERNAME] = gmailUserName;
            this.Fields[SEND_PASSWORD] = gmailPassword;

            _gmailUserName = gmailUserName;
            _gmailPassword = gmailPassword;
        }

        /// <summary>
        /// Sends the message. If no from address is given the message will be from <c>GmailUserName</c>@Gmail.com
        /// </summary>
        public void Send()
        {
            try
            {
                if (this.From == string.Empty)
                {
                    this.From = GmailUserName;
                    if (GmailUserName.IndexOf('@') == -1) this.From += "@Gmail.com";
                }

                System.Web.Mail.SmtpMail.Send(this);
            }
            catch (Exception ex)
            {
                //TODO: Add error handling
                throw ex;
            }
        }

        /// <summary>
        /// The username of the gmail account that the message will be sent through
        /// </summary>
        public string GmailUserName
        {
            get { return _gmailUserName; }
            set { _gmailUserName = value; }
        }

        /// <summary>
        /// The password of the gmail account that the message will be sent through
        /// </summary>
        public string GmailPassword
        {
            get { return _gmailPassword; }
            set { _gmailPassword = value; }
        }

        #endregion

        #region Static Members

        /// <summary>
        /// Send a <c>System.Web.Mail.MailMessage</c> through the specified gmail account
        /// </summary>
        /// <param name="gmailUserName">The username of the gmail account that the message will be sent through</param>
        /// <param name="gmailPassword">The password of the gmail account that the message will be sent through</param>
        /// <param name="message"><c>System.Web.Mail.MailMessage</c> object to send</param>
        /*public static void SendMailMessageFromGmail(string gmailUserName, string gmailPassword, MailMessage message)
        {
            try
            {
                message.Fields[SMTP_SERVER] = GmailMessage.GmailServer;
                message.Fields[SMTP_SERVER_PORT] = GmailMessage.GmailServerPort;
                message.Fields[SEND_USING] = 2;
                message.Fields[SMTP_USE_SSL] = true;
                message.Fields[SMTP_AUTHENTICATE] = 1;
                message.Fields[SEND_USERNAME] = gmailUserName;
                message.Fields[SEND_PASSWORD] = gmailPassword;

                System.Web.Mail.SmtpMail.Send(message);
            }
            catch (Exception ex)
            {
                //TODO: Add error handling
                throw ex;
            }
        }*/

        /// <summary>
        /// Sends an email through the specified gmail account
        /// </summary>
        /// <param name="gmailUserName">The username of the gmail account that the message will be sent through</param>
        /// <param name="gmailPassword">The password of the gmail account that the message will be sent through</param>
        /// <param name="toAddress">Recipients email address</param>
        /// <param name="subject">Message subject</param>
        /// <param name="messageBody">Message body</param>
        public static void SendFromGmail(string gmailUserName, string gmailPassword, string toAddress, string subject, string messageBody)
        {
            try
            {
                GmailMessage gMessage = new GmailMessage(gmailUserName, gmailPassword);
                gMessage.To = toAddress;
                gMessage.Subject = subject;
                gMessage.Body = messageBody;
                gMessage.From = gmailUserName;
                if (gmailUserName.IndexOf('@') == -1) gMessage.From += "@gmail.com";

                /*gMessage.To = toAddress;
                gMessage.Subject = subject;
                gMessage.Body = messageBody;
                gMessage.From = gmailUserName;
                if (gmailUserName.IndexOf('@') == -1) gMessage.From += "@Gmail.com";
                //gMessage.BodyFormat = System.Net.Mail.MailMessage.IsBodyHtml;//System.Web.Mail.MailFormat.Html;//System.Net.Mail.MailMessage.IsBodyHTML

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(gMessage.To);
                mail.From = new MailAddress(gMessage.From, "Email head", System.Text.Encoding.UTF8);
                mail.Subject = gMessage.Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = gMessage.Body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(gmailUserName, gmailPassword);
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;*/

                MailMessage mm = new MailMessage(gMessage.From, gMessage.To);

                mm.Subject = gMessage.Subject;
                mm.Body = gMessage.Body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(gMessage.From, gMessage.GmailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

                //System.Web.Mail.SmtpMail.Send(gMessage);

                //client.Send(mail);
            }
            catch (Exception ex)
            {
                //TODO: Add error handling
                throw ex;
            }
        }

        /// <summary>
        /// The name of the gmail server, the default is "smtp.gmail.com"
        /// </summary>
        public static string GmailServer
        {
            get { return _gmailServer; }
            set { _gmailServer = value; }
        }

        /// <summary>
        /// The port to use when sending the email, the default is 465
        /// </summary>
        public static long GmailServerPort
        {
            get { return _gmailPort; }
            set { _gmailPort = value; }
        }

        #endregion
    }
}