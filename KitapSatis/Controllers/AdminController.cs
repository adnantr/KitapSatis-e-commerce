using KitapSatis.Data;
using KitapSatis.Identity;
using KitapSatis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapSatis.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AdminController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonmembers = new List<User>();
            foreach (var item in _userManager.Users)
            {
                var list=await _userManager.IsInRoleAsync(item, role.Name)
                    ?members:nonmembers;
                list.Add(item);
                
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        { 
            if(ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[]{})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }


                foreach (var userId in model.IdsToDelete ?? new string[] {})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }
            }
            return Redirect(model.RoleId);
            
        }
       //roledelete yapılacak


        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
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
                    TempData["message"] = $"{obj.ProductName} isimli ürün eklenmiştir";
                }
                else
                {
                    //Güncelleme işlemi
                    _db.Products.Update(obj);
                    TempData["message"] = $"{obj.ProductName} isimli ürün güncellenmiştir";

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
            TempData["message"] = $"{objDb.ProductName} isimli ürün silinmişir";
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
                    TempData["message"] = $"{obj.CategoryName} isimli kategori eklenmiştir";
                }
                else
                {
                    //Güncelleme işlemi
                    _db.Categories.Update(obj);
                    TempData["message"] = $"{obj.CategoryName} isimli kategori güncellenmiştir";
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Category));
            }

            return View(obj);
        }


        public IActionResult DeleteCategory(int id) //route-Id den alıyor
        {
            var products = _db.Products.ToList();
            foreach (var product in products)
            {
                if (product.CategoryId == id)
                {
                    TempData["message"] = " Silinecek kategoriye kayıtlı bir ürün bulunmaktadır lütfen önce onu siliniz.";
                    return RedirectToAction(nameof(Category));
                }
                else
                {
                    var productControl = _db.Products.Where(a => a.CategoryId == id).ToList();

                    if (!productControl.Any())
                    {
                        var objDb = _db.Categories.FirstOrDefault(a => a.CategoryId == id);
                        _db.Categories.Remove(objDb);
                        _db.SaveChanges();
                        return RedirectToAction(nameof(Category));
                    }
                    else
                    {

                    }
                }
            }
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
                    TempData["message"] = $"{obj.KindName} isimli tür eklenmiştir";
                }
                else
                {
                    //Güncelleme işlemi
                    _db.Kinds.Update(obj);
                    TempData["message"] = $"{obj.KindName} isimli tür güncellenmiştir";
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
                    TempData["message"] = " Silinecek türe kayıtlı bir ürün bulunmaktadır lütfen önce onu siliniz.";
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
                        return RedirectToAction(nameof(Category));
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
        }  //SİLL

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var selectedRoles=await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName=user.UserName,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Email=user.Email,
                    EmailConfirmed=user.EmailConfirmed,
                    SelectedRoles=selectedRoles
                });
            }
            return Redirect("~/admin/UserList");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model,string[] selectedRoles)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("/admin/UserList");
                    }
                }
                return Redirect("/admin/UserList");
            }
            return View(model);
        }
    }
}
