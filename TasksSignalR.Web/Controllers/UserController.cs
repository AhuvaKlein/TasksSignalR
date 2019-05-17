using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TasksSignalR.Data;

namespace TasksSignalR.Web.Controllers
{
    public class UserController : Controller
    {
        private string _connectionString;
        private UserRepository _urepo;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _urepo = new UserRepository(_connectionString);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (_urepo.LoginVerify(user))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", user.Email)
                };

                HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

                return Redirect("/home/index");
            }
            else
            {
                return Redirect("/user/login");
            }
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            _urepo.AddUser(user);
            return Redirect("/home/index");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/user/login");
        }

    }
}