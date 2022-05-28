using KitapSatis.Data;
using KitapSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace KitapSatis.Controllers
{
    public class SharedController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SharedController(ApplicationDbContext context)
        {
            _context = context;

        }

     
        public IActionResult Index()
        {
            return View();
        }
    }
}
