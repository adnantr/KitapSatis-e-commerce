using KitapSatis.Data;
using KitapSatis.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KitapSatis.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? id, string q)  //arama çubuğu için q yazdık
        {
            List<Product> categoryList = _db.Products.ToList();
            if (id != null)      //Filtreli kategoriler listelenir
            {
                categoryList = categoryList.Where(p => p.CategoryId == id).ToList();
            }
            if (!string.IsNullOrEmpty(q))
            {
                categoryList = categoryList.Where(i => i.ProductName.ToLower().Contains(q.ToLower()) || i.ProductDescription.Contains(q)).ToList();
            }
            return View(categoryList);
        }

        public IActionResult Details(int? id)
        {
            //List<Product> productList = _db.Products;
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var productdetails = _db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if(productdetails==null)
            {
                return NotFound();
            }
            return View(productdetails);
        }
    }
}
