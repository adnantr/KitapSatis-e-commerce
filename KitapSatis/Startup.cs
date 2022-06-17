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

namespace KitapSatis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();
           
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")
            ));
            services.AddDbContext<ApplicationUserContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")
                ));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationUserContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true; //parolanýn içerisinde sayý olmasýný zorlarýz
                options.Password.RequireLowercase = true;//büyük harf zorlama
                options.Password.RequireUppercase = true;//küçük harf zorlama
                options.User.RequireUniqueEmail = true; //her kullanýcýnýn farklý maili olmasýný zorlar
                options.SignIn.RequireConfirmedEmail = false;   //e-posta doðrulama
            });

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(x =>
            //    {
            //        x.LoginPath = "/customer/Login/";
            //        x.LogoutPath = "/customer/logout/";
            //    });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login/";
                options.LogoutPath = "/Account/logout/";
                options.AccessDeniedPath = "/Account/AccessDenied"; //yetki (SAYFA HAZIRLA)
                options.SlidingExpiration = true; //süreli oturum
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
