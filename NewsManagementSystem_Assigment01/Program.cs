using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Repositories;
using NewsManagementSystem_Assigment01.Services;

namespace NewsManagementSystem_Assigment01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<FunewsManagementContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FUNewsManagement"));
            });

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();  
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<INewsRepository, NewsRepository>();
            builder.Services.AddScoped<NewsService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<TagService>();


            // 3) Add Authentication with Cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";  // Trang đăng nhập
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Trang bị từ chối
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(100);
                    // Gia hạn cookie nếu còn hoạt động
                    options.SlidingExpiration = true;
                });

            //builder.Services.AddIdentity<SystemAccount, IdentityRole>()
            //    .AddEntityFrameworkStores<FunewsManagementContext>()
            //    .AddDefaultTokenProviders();

            //builder.Services.AddAuthorization(options =>
            //{
            //    // Example policies if you want to use policy-based approach
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
            //    options.AddPolicy("Lecturer", policy => policy.RequireRole("Lecturer"));
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
