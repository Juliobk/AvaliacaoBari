using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Logging;
using Models;

namespace Receiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageDisplayController : Controller
    {
        public MessageDisplayController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            TempData.Keep("Message");
            if (TempData["Message"] == null)
            {
                TempData["Message"] = Guid.NewGuid().ToString();
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Message message)
        {
            TempData["Message"] = message.Text;
            return View();
        }
    }
}
