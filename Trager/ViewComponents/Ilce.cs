using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.ViewComponents
{
    public class Ilce : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
