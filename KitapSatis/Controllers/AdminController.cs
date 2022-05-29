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
            //ViewBag.categoryName = _db.Categories.ToList();
            List<Product> objList = _db.Products.ToList();
            return View(objList);

        }
       
        public IActionResult AddUpdateProduct(int? id)
        {
            ViewBag.kindlist= _db.Kinds.ToList();
            ViewBag.categorylist = _db.Categories.ToList();
            Product obj = new Product();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Products.FirstOrDefault(a => a.ProductId == id);
            if (obj == null)
            {
                return NotFound(); //hata
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Güvenlik amacıyla post metotlarında kullan-araştır
        public IActionResult AddUpdateProduct(Product obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.ProductId == 0)
                {
                    //Ekleme işlemi
                    _db.Products.Add(obj);
                }
                else
                {
                    //Güncelleme işlemi
                    _db.Products.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Product));
            }

            return View(obj);
        }

        public IActionResult DeleteProduct(int id) //route-Id den alıyor
        {
            var objDb = _db.Products.FirstOrDefault(a => a.ProductId == id);
            _db.Products.Remove(objDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Product));
        }

        public IActionResult Category()
        {
            ViewBag.categorylist = _db.Categories;
            ViewBag.kindlist = _db.Kinds.ToList();
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }
        public IActionResult AddUpdateCategory(int? id)
        {
            Category obj = new Category();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Categories.FirstOrDefault(a => a.CategoryId == id);
            if (obj == null)
            {
                return NotFound(); //hata
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUpdateCategory(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CategoryId == 0)
                {
                    //Ekleme işlemi
                    _db.Categories.Add(obj);
                }
                else
                {
                    //Güncelleme işlemi
                    _db.Categories.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Category));
            }

            return View(obj);
        }
        public IActionResult DeleteCategory(int id) //route-Id den alıyor KIND İLE BENZERE ÇEVİR
        {
            var objDb = _db.Categories.FirstOrDefault(a => a.CategoryId == id);
            _db.Categories.Remove(objDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Category));
        }

        public IActionResult AddUpdateKind(int? id) //Category.cshtml den geliyor
        {
            Kind obj = new Kind();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Kinds.FirstOrDefault(a => a.KindId == id);
            if (obj == null)
            {
                return NotFound(); //hata
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUpdateKind(Kind obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.KindId == 0)
                {
                    //Ekleme işlemi
                    _db.Kinds.Add(obj);
                }
                else
                {
                    //Güncelleme işlemi
                    _db.Kinds.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Category));
            }

            return View(obj);
        }
        public IActionResult DeleteKind(int id) //route-Id den alıyor
        {
            var products = _db.Products.ToList();
            foreach (var product in products)
            {
                if (product.KindId == id)
                {
                    //string message = "Silinecek veride kayıtlı ürünler bulunmakta önce ürünleri siliniz.";
                    return RedirectToAction(nameof(Category));

                }
                else
                {
                    var productControl = _db.Products.Where(a => a.KindId == id).ToList();

                    if (!productControl.Any())
                    {
                        var objDb = _db.Kinds.FirstOrDefault(a => a.KindId == id);
                        _db.Kinds.Remove(objDb);
                        _db.SaveChanges();
                        //string message = "Başarıla silinmiştir";
                    }
                    else
                    {

                    }


                }
            }
            return RedirectToAction(nameof(Category));

        }
        public IActionResult WeekProduct(int ProductId)
        {
          
            return View();
        }


    }
}
