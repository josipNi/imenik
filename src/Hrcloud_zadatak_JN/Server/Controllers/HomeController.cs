using Microsoft.AspNetCore.Mvc;

namespace Imenik_JN.Server.Controllers
{
    public class HomeController:Controller
    {      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
