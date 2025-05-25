using EmployeeManagementApplication.Data;
using EmployeeManagementApplication.IServices;
using EmployeeManagementApplication.Repository.Interface;
using EmployeeManagementApplication.Repository.Services;
using EmployeeManagementApplication.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeManagementApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<EmployeeDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmployee, EmployeeService>();



            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
     {
         
         options.LoginPath = "/Auth/Login";

        
         options.AccessDeniedPath = "/Auth/AccessDenied";

        
         options.Cookie.Name = "MyAppAuthCookie";

        
         options.ExpireTimeSpan = TimeSpan.FromHours(1);

         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        
         options.SlidingExpiration = true;
     });

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Demo}/{id?}");

            app.Run();
        }
    }
}
