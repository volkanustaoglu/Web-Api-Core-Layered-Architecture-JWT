using Microsoft.AspNetCore.Mvc;

namespace WebCoreClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
