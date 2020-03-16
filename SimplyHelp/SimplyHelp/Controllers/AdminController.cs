using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplyHelp.Models;

namespace SimplyHelp.Controllers
{
    public class AdminController : Controller
    {
        [BindProperty]
        public Users UserA { get; set; }

        private readonly SimplyHelpContext _context;

        public AdminController(SimplyHelpContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> UserList { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            UserList = _context.Role.ToList();
            ViewBag.DropDownList = UserList;

            UserA = new Users();
            if (id == null)
            {
                //create 
                return View(UserA);
            }

            //update
            UserA = _context.Users.FirstOrDefault(u => u.Id == id);
            if (User == null)
            {
                return NotFound();
            }
            return View(UserA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {           

            if (ModelState.IsValid)
            {
                if (UserA.Id == 0)
                {
                    _context.Users.Add(UserA);
                }
                else
                {
                    _context.Users.Update(UserA);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(User);
        }
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //UserList = await _context.Users.ToListAsync();

            return Json(new { data = await _context.Users.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _context.Users.Remove(bookFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Deleted Successful" });
        }
        #endregion
    }
}