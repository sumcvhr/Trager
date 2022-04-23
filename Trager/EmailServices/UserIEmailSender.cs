using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.EmailServices
{
    public interface UserIEmailSender
    {
        // smtp => gmail, hotmail
        // api  => sendgrid

        Task SendEmailAsync(string usereremail, string usersubject, string userhtmlMessage);

    }
}
