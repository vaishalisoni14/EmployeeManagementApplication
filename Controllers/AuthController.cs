using EmployeeManagementApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmployeeManagementApplication.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using EmployeeManagementApplication.Models;
using EmployeeManagementApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementApplication.Controllers
{

    public class AuthController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public AuthController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
      
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/")]
        public IActionResult Login(string username, string password, [FromQuery] string? ReturnUrl)
        {
            var parameters = new[]
            {
                new SqlParameter("@email" ,SqlDbType.NVarChar){Value =username},
                new SqlParameter("@password" , SqlDbType.NVarChar){Value=password}

            };
            var test = _employeeDbContext.Set<UserView>().FromSqlRaw($"EXEC spPractice @email,@password", parameters).IgnoreQueryFilters().AsEnumerable().FirstOrDefault();
            
           
            if (test!=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                var redirectUrl = Request.Query["Login.cshtml"];
                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }
                return RedirectToAction("Demo", "Home");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
