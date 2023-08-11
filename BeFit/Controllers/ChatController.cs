using BeFit.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Chatting()
        {
            return this.View("Chat");
        }
    }
}
