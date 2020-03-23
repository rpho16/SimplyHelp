using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyHelp.Models;
using SimplyHelp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace SimplyHelp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SimplyHelpContext _context;     
        public AccountController(SimplyHelpContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.Users
                    .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }

                HttpContext.Session.SetString("userName", userdetails.FullName);
                HttpContext.Session.SetString("userId", userdetails.Id.ToString());
                HttpContext.Session.SetString("userRole", userdetails.IdRole.ToString());

                var userName = HttpContext.Session.GetString("userName");
                var userId = HttpContext.Session.GetString("userId");
                var userRole = HttpContext.Session.GetString("userRole");

                if (userdetails.IdRole == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (userdetails.IdRole == 2)
                {
                    return RedirectToAction("Index", "Manager");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return View("Index");
            }            
        }
        // registration Page load
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Users model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    IdRole = 3
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            else
            {
                return View(model);
            }
            ModelState.Clear();
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }
        }
    }
}