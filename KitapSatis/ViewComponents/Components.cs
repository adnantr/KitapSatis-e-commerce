using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitapSatis.Controllers;
using KitapSatis.Data;
using KitapSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KitapSatis.ViewComponents
{
    

    [ViewComponent]
    public class CategoryNavbarComponents : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CategoryNavbarComponents(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }
        
    }

    public class _footerComponents : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public _footerComponents(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }
    }

}
