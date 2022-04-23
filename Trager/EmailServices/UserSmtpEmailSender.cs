using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Trager.EmailServices
{
    public class UserSmtpEmailSender: UserIEmailSender
    {
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _useremail;
        private string _password;

        public UserSmtpEmailSender()
        {
        }

        public UserSmtpEmailSender(string host, int port, bool enableSSL, string useremail, string userpassword)
        {
            this._host = host;
            this._port = port;
            this._enableSSL = enableSSL;
            this._useremail = useremail;
            this._password = userpassword;
        }
        public Task SendEmailAsync(string useremail, string usersubject, string userhtmlMessage)
        {
            var client = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(_useremail, _password),
                EnableSsl = this._enableSSL
            };

            return client.SendMailAsync(
                new MailMessage(this._useremail, useremail, usersubject, userhtmlMessage)
                {
                    IsBodyHtml = true
                }
            );
        }
    }
}
