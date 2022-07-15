using KitapSatis.Abstract;
using KitapSatis.Data;
using KitapSatis.Identity;
using KitapSatis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KitapSatis.Controllers
{
    [Authorize(Roles = "Customer")]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteService _favoriteService;
        public FavoriteController(ApplicationDbContext db, UserManager<User> userManager, IFavoriteService favoriteService)
        {
            _db = db;
            _userManager = userManager;
            _favoriteService = favoriteService;
        }
        public IActionResult Index()
        {
            var favorite = _favoriteService.GetFavoriteByUserId(_userManager.GetUserId(User));

            return View(new FavoriteModel()
            {
                FavoriteId = favorite.Id,
                FavoriteItems = favorite.FavoriteItems.Select(i => new FavoriteItemModel()
                {
                    FavoriteItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.ProductName,
                    Price = (double)i.Product.Price,
                    ImageUrl = i.Product.Picture,
                }).ToList()

            }) ;
        }
        [HttpPost]
        public IActionResult AddFavorite(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _favoriteService.AddFavorite(userId,productId);
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult DeleteFromFavorite(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _favoriteService.DeleteFromFavorite(userId, productId);
            return RedirectToAction("index");
        }

    }
}
