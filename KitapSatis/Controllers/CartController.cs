using KitapSatis.Abstract;
using KitapSatis.Data;
using KitapSatis.Identity;
using KitapSatis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace KitapSatis.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
        public CartController(ApplicationDbContext db, ICartService cartService, UserManager<User> userManager)
        {
            _db = db;
            _cartService =cartService;
            _userManager =userManager;
        }
        public IActionResult Index()
        {
            var cart=_cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId=i.Id,
                    ProductId=i.ProductId,
                    Name=i.Product.ProductName,
                    Price=(double)i.Product.Price,
                    ImageUrl=i.Product.Picture,
                    Quantity=i.Quantity
                }).ToList()

            }); ;
        }
        [HttpPost]
        public IActionResult AddCart(int productId,int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddCart(userId,productId,quantity);
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, productId);
            return RedirectToAction("index");
        }
    }
}
