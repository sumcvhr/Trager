using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Controllers
{
    public class BaseController : Controller
    {
        public Models.Courier OturumBlgi { 
            get {
                string oturumBilgi = HttpContext.Session.GetString("oturum");
                if (oturumBilgi == null)
                {
                    return null;
                }
                var oturum = JsonConvert.DeserializeObject<Models.Courier>(oturumBilgi);
                return oturum;
            } }

        public Models.User UserOturumBlgi
        {
            get
            {
              
                string UseroturumBilgi = HttpContext.Session.GetString("useroturum");
                if (UseroturumBilgi == null)
                {
                    return null;
                }
                var useroturum = JsonConvert.DeserializeObject<Models.User>(UseroturumBilgi);
                return useroturum;
            }
        }
    }
}
