using KitapSatis.Abstract;
using KitapSatis.Data;
using KitapSatis.Email;
using KitapSatis.Identity;
using KitapSatis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KitapSatis.Controllers
{
    [AutoValidateAntiforgeryToken]
    
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager; //user işlemleri
        private readonly SignInManager<User> _singInManager;//cookie işlemleri
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailSend;
        private readonly ICartService _cartService;
        private readonly IFavoriteService _favoriteService;

        public AccountController(ApplicationDbContext db, UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailSend, ICartService cartService, IFavoriteService favoriteService, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;       
            _singInManager = signInManager;
            _emailSend = emailSend;
            _cartService = cartService;
            _favoriteService = favoriteService;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl=null)
        {
            return View(new LoginModel()
            { 
                ReturnUrl=ReturnUrl            
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı adı yok");
                return View(model);
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen mailinize gelen link ile hesabınızı onaylayınız");
                return View(model);
            }

            var result = await _singInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış!");
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
            
                return View(model);
            }
            
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var addRoleToUser = await _userManager.AddToRoleAsync(user, "Customer");
            if (result.Succeeded)
            {         
                _cartService.InitializeCart(user.Id); //kullanıcıya sepet atama
                _favoriteService.InitializeFavorite(user.Id); //kullanıcıya favori atama
                
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user); //token üretme
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                await _emailSend.SendEmailAsync(model.Email, "Hesabını onaylayınız.", $"Email hesabını onaylamak için <a href='https://localhost:44389{url}'>linke</a> tıklayınız");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId==null || token ==null)
            {
                TempData["message"] = "Geçersiz doğrulama kodu. Lütfen doğru kodu giriniz!";
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız başarıyla onaylanmıştır.";
                    return View();
                }   
            }
            TempData["message"] = "Maalesef hesabınız onaylanmadı.";
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if(user==null)
            {
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            await _emailSend.SendEmailAsync(Email, "Şifre sıfırlama bağlantıısı.", $"Şifrenizi sıfırlamak için <a href='https://localhost:44389{url}'>linke</a> tıklayınız");


            return View();
        }
        public IActionResult ResetPassword(string userId,string token)
        {
            if(userId==null|| token==null)
            {
                return RedirectToAction("Home","Index");
            }
            var model = new ResetPasswordModel { Token = token };

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user==null)
            {
                return RedirectToAction("Home", "Index");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }      
            }
            return View(model);
        }
    }
}
