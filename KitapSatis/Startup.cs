using KitapSatis.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KitapSatis.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using KitapSatis.Email;
using KitapSatis.Repository;
using KitapSatis.Abstract;
using KitapSatis.Concrete;

namespace KitapSatis
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();

            services.AddScoped<ICartRepository, EfCartRepository>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IFavoriteRepository, EfFavoriteRepository>();
            services.AddScoped<IFavoriteService, FavoriteManager>();



            services.AddScoped<IEmailService, Hotmail>(i =>
                new Hotmail(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"])
                );
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection")
            ));
            services.AddDbContext<ApplicationUserContext>(options => options.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection")
                ));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationUserContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true; //parolan�n i�erisinde say� olmas�n� zorlar�z
                options.Password.RequireLowercase = true;//b�y�k harf zorlama
                options.Password.RequireUppercase = true;//k���k harf zorlama
                options.User.RequireUniqueEmail = true; //her kullan�c�n�n farkl� maili olmas�n� zorlar
                options.SignIn.RequireConfirmedEmail = true;   //e-posta do�rulama
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login/"; //giri�
                options.LogoutPath = "/Account/logout/"; //��k��
                options.AccessDeniedPath = "/Account/AccessDenied"; //yetkisiz giri�
                options.SlidingExpiration = false; //s�reli oturum
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".KitapSatis.Security.Cookie",
                    SameSite=SameSiteMode.Strict
                };

            });
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                    RequestPath="/modules"                
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
