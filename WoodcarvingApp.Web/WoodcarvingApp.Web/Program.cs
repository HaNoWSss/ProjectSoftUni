using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Web.Data;

namespace WoodcarvingApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("SQLServer") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Add services to the container.
            builder.Services.AddDbContext<WoodcarvingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
            });


            builder.Services//
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
                {

                })
                .AddEntityFrameworkStores<WoodcarvingDbContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(cfg =>//
            {
                cfg.LoginPath = "/Identity/Account/Login";
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(); //

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
