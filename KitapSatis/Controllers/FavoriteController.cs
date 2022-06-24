using KitapSatis.Data;
using KitapSatis.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KitapSatis.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
        public FavoriteController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
