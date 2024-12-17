using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;
using WoodcarvingApp.Data.Repository;
using WoodcarvingApp.Data.Repository.Interfaces;
using WoodcarvingApp.Services.Data;
using WoodcarvingApp.Services.Data.Interfaces;
using WoodcarvingApp.Services.Mapping;
using WoodcarvingApp.Web.Data;
using WoodcarvingApp.Web.Infrastructure.Extensions;
using WoodcarvingApp.Web.Models;

namespace WoodcarvingApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("SQLServer")!;
            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:Username")!;
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;
            // Add services to the container.
            builder.Services.AddDbContext<WoodcarvingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
            });


            builder.Services//
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
                {
                    ConfigureIdentity(builder, cfg);
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

            // builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            //builder.Services.RegisterUserDefinedServices(typeof(IWoodcarvingService).Assembly);

            builder.Services.AddScoped<IWoodcarvingRepository, WoodcarvingRepository>();
            builder.Services.AddScoped<IWoodcarverRepository, WoodcarverRepository>();
            builder.Services.AddScoped<IRepository<WoodType, Guid>, BaseRepository<WoodType, Guid>>();
            builder.Services.AddScoped<IRepository<City, Guid>, BaseRepository<City, Guid>>();
            builder.Services.AddScoped<IExhibitionRepository, ExhibitionRepository>();

            // Register your services
            builder.Services.AddScoped<IWoodcarvingService, WoodcarvingService>();
            builder.Services.AddScoped<IWoodcarverService, WoodcarverService>();
            builder.Services.AddScoped<IWoodTypeService, WoodTypeService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(); //

            WebApplication app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

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

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

            if (app.Environment.IsDevelopment())
            {
                app.SeedAdministrator(adminEmail, adminUsername, adminPassword);
            }

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "Errors",
                pattern: "{controller=Home}/{action=Index}/{statusCode?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }


        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
        {
            cfg.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            cfg.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            cfg.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
            cfg.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            cfg.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireDigits");
            cfg.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}
