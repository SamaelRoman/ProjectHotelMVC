using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectHotelMVC.Filters;
using ProjectHotelMVC.Interfaces;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAuthService authService;
        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            this.authService = authService;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            EmplpoyeeGetTokenViewModel employee = new EmplpoyeeGetTokenViewModel() {
                Login = null,
                Password = null
            };
            return View("Login",employee);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(EmplpoyeeGetTokenViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string Token = await authService.GetToken(employee);
                if(Token != null)
                {
                    Token = Token.Replace("\"",string.Empty);
                    Token = Token.Replace("\\", string.Empty);
                    HttpContext.Session.Set("ValidationJWT", Encoding.UTF8.GetBytes(Token));
                    return RedirectPermanent("~/Admin/Index");
                }
                ViewBag.AuthMSG = "Ошибка Авторзации проверьте верность указанных данных!";
                return RedirectPermanent("Login");
            }
            else
            {
                ViewBag.AuthMSG = "Логин или Пароль не соответствуют требованиям!";
                return RedirectPermanent("Login");
            }
        }
        [HttpGet]
        [Authorize("Administrator", "Moderator")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("ValidationJWT");
            HttpContext.Items["Employee"] = null;
            return View("SuccessLogOut");

        }
    }
}
