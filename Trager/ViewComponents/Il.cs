using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.ViewComponents
{
    public class Il : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
