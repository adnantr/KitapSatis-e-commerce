using Microsoft.AspNetCore.Mvc;

namespace KitapSatis.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
