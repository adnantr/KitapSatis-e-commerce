using KitapSatis.Data;
using KitapSatis.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KitapSatis.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        //ApplicationDbContext c = new ApplicationDbContext();
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Customer p)
        {
            var information = _db.Customers.FirstOrDefault(x => x.CustomerName == p.CustomerName && x.Password == p.Password);
            if (information == null)
            {
                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name,p.CustomerName)
                };
                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Register(int? id)
        {
            Customer obj = new Customer();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Customers.FirstOrDefault(a => a.CustomerId == id);
            if (obj == null)
            {
                return NotFound(); //hata
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CustomerId == 0)
                {
                    //Ekleme işlemi
                    _db.Customers.Add(obj);
                }
                _db.SaveChanges();

                return RedirectToAction(nameof(Register));
            }

            return View(obj);
        }
        [Authorize]
        public IActionResult Favorite()
        {
            return View();
        }
        [Authorize]
        public IActionResult ShoppingBasket()
        {
            return View();
        }
    }
}
