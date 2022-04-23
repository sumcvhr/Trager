using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Trager.EmailServices
{
    public interface CourierIEmailSender
    {
        // smtp => gmail, hotmail
        // api  => sendgrid

        Task SendEmailAsync(string courieremail, string couriersubject, string courierhtmlMessage);

    }
}
