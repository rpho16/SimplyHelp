using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Data;
using System.Data.SqlClient;
using SimplyHelp.Models.ViewModel;
using SimplyHelp.Models.TableViewModel;
using SimplyHelp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimplyHelp.Controllers
{
    [EnableCors("allow")]
    public class UserController : Controller
    {
        public IConfiguration Configuration { get; }
        private readonly SimplyHelpContext _context;
        public IEnumerable<UserMembers> UserMemberList { get; set; }
        public IEnumerable<PlacesGeo> PlacesGeoList { get; set; }
        public IEnumerable<UserGeo> UserGeoList { get; set; }
        public string UserMemberssList { get; set; }
        public UserController(IConfiguration configuration, SimplyHelpContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {   
            int UserIDMembers = Convert.ToInt32(TempData["UserId"]);
            //On a class user HttpContext
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var userName = HttpContext.Session.GetString("userName");

            UserMemberList = _context.UserMembers.Where(d => d.UserId == userId).OrderBy(d => d.Id).ToList();
            PlacesGeoList = _context.PlacesGeo.Where(d => d.UserId == userId).GroupBy(d => new { d.PlaceName, d.PlaceType, d.PlaceVicinity}).Select(d => d.First()).ToList(); 
            ViewBag.ListMemb = UserMemberList;           
            ViewBag.ListLocation = PlacesGeoList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("FullName, Latitude,Longitude")] [FromBody]UserMembersView model)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var userName = HttpContext.Session.GetString("userName");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserGeoList = _context.UserGeo.Where(d => d.Userid == userId).GroupBy(d => new { d.Latitude, d.Longitude }).Select(d => d.First()).ToList();
            UserMemberssList = _context.UserMembers.Where(x => x.FullName == userName).Select(x => x.FullName).ToString();

            if (!String.IsNullOrEmpty(UserMemberssList))
            {
                if (UserGeoList != null)                
                {
                    string myDb1ConnectionString = Configuration.GetConnectionString("DevConnection");

                    foreach (var item in UserGeoList)
                    {
                        using (SqlConnection connection = new SqlConnection(myDb1ConnectionString))
                        {
                            string sql = $@"Update UserMembers set latitude = '{item.Latitude}', longitude = '{item.Longitude}' 
                                            Where fullName like '{userName}'";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.CommandType = CommandType.Text;
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                    }
                }
            }            
            return Content("1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([FromBody]UserGeo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.UserGeo.Add(model);
            _context.SaveChanges();
            ModelState.Clear();
            return Content("1");           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPlaces([FromBody]PlacesGeo model)
        {
            //int UserIDMembers = Convert.ToInt32(TempData["UserId"]);
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            if (!ModelState.IsValid)
            {
                return View(model);
            }            
            _context.PlacesGeo.Add(model);
            _context.SaveChanges();
            ModelState.Clear();
            return Content("1");
        }
        public IActionResult UserMembers(UserMembers model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.UserMembers.Add(model);
            _context.SaveChanges();
            TempData["UserId"] = model.UserId;
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult GetMembersLoc()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            UserMemberList = _context.UserMembers.Where(d => d.UserId == userId).OrderBy(d => d.Id).ToList();
            ViewBag.ListMembGeo = UserMemberList;

            return Ok(UserMemberList);
        }
    }
}
