using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    public class AlertMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string AlertType { get; set; }
    }
}
