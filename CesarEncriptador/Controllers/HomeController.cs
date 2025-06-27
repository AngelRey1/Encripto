using Microsoft.AspNetCore.Mvc;

namespace CesarEncriptador.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
    }
} 