using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Trager.EmailServices
{
    public class CourierSmtpEmailSender : CourierIEmailSender
    {
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string CourierName;
        private string _password;

        public CourierSmtpEmailSender()
        {
        }

        public CourierSmtpEmailSender(string host, int port, bool enableSSL, string CourierName, string CourierPassword)
        {
            this._host = host;
            this._port = port;
            this._enableSSL = enableSSL;
            this.CourierName = CourierName;
            this._password = CourierPassword;
        }
        public Task SendEmailAsync(string CourierEmail, string subject, string htmlMessage)
        {
            var client = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(CourierName, _password),
                EnableSsl = this._enableSSL
            };

            return client.SendMailAsync(
                new MailMessage(CourierName, CourierEmail, subject, htmlMessage)
                {
                    IsBodyHtml = true
                }
            );
        }
    }
}
