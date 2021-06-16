using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reifen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reifen.Controllers
{
    public class LoginController : Controller
    {
        private readonly ReifenContext context;

        public LoginController(ReifenContext _context)
        { context = _context; }

        public IActionResult Login(bool? loginresult)
        {
            if (loginresult!=null && loginresult==false)
            {
                ViewBag.Message = "Bitte überprüfen Sie Ihre Anmeldeinformationen.!";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var model = context.Personals.Where(p => p.UserName == username && p.Password == password && p.isActive==true).FirstOrDefault();
            if (model != null)
            {
                CookieOptions cookie = new CookieOptions();
                Response.Cookies.Append("username", username);
                if (model.isManager == true)
                  Response.Cookies.Append("role", "manager");
                else
                  Response.Cookies.Append("role", "personal");
                
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Login", new { loginResult = false }); ;
        }

        public IActionResult Logout()
        {
            DeleteCookies();
            return RedirectToAction("Login","Login");
        }

        private void DeleteCookies()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
        }
    }
}
