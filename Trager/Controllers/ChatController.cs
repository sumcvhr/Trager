using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trager.Models;

namespace Trager.Controllers
{
    public class ChatController : BaseController
    {
        private readonly ApplicationContext _context;
        public ChatController(ApplicationContext context)
        {
        
            _context = context;
        }
    
        [HttpPost]
        public IActionResult Chat(Chat model)
        {
            if (UserOturumBlgi != null || OturumBlgi != null)
            {
                model.CourierId = OturumBlgi.Id;
                model.UserId = UserOturumBlgi.Id;
                _context.Chat.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("Chat");
        }
    }
}
