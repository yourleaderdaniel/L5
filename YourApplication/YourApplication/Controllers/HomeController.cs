using Microsoft.AspNetCore.Mvc;
using System;

namespace YourApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string value, DateTime expireTime)
        {
            var combinedValue = $"{value}|{expireTime:O}"; // Использование формата "O" для универсального формата времени
            Response.Cookies.Append("MyCookie", combinedValue, new CookieOptions { Expires = expireTime });
            return RedirectToAction("CheckCookie");
        }

        public IActionResult CheckCookie()
        {
            var cookieValue = Request.Cookies["MyCookie"];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                var parts = cookieValue.Split('|');
                if (parts.Length == 2)
                {
                    ViewBag.Value = parts[0];
                    ViewBag.ExpireTime = DateTime.Parse(parts[1]).ToString("g"); // Преобразование строки обратно в DateTime и форматирование для отображения
                    ViewBag.HasFormData = true;
                }
            }
            else
            {
                ViewBag.HasFormData = false;
            }
            return View();
        }


    }
}
