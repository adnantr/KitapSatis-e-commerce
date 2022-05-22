using KitapSatis.Data;
using KitapSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace KitapSatis.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product()
        {
            List<Product> objList = _db.Products.ToList();
            return View(objList);
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult UpdateProduct(int? id)
        {
            Product obj = new Product();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Products.First(a => a.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Category()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }


    }
}
