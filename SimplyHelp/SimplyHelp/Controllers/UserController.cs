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
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {            
            
            List<MembersTableViewModel> memLst = null;
            List<PlacesGeoModel> placesLocation = null;


            int UserIDMembers = Convert.ToInt32(TempData["UserId"]);
            //On a class user HttpContext
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var userName = HttpContext.Session.GetString("userName");

            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                memLst = (from d in db.UserMembers
                             where d.UserId == userId
                                orderby d.Id
                             select new MembersTableViewModel
                             {
                                 Fullname = d.FullName,
                                 PhoneNumber = d.PhoneNumber,
                                 Email = d.Email,
                                 ZipCode = d.ZipCode
                             }).ToList();
            }

            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                placesLocation = (from d in db.PlacesGeo
                                  where d.UserId == userId
                                  orderby d.PlaceType
                                  select new PlacesGeoModel
                                  {
                                      PlaceName = d.PlaceName,
                                      PlaceVicinity = d.PlaceVicinity,
                                      PlaceType = d.PlaceType
                                  }).Distinct().ToList();
            }
            
            ViewBag.ListMemb = memLst;
            ViewBag.ListLocation = placesLocation;

            //RedirectToAction("Edit");

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("FullName, Latitude,Longitude")] [FromBody]UserMembersView model)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var userName = HttpContext.Session.GetString("userName");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                var lastLoca = db.UserGeo
                    .Where(x => x.Userid == userId)
                    .OrderByDescending(x => x.Userid)
                    .FirstOrDefault();

                string locMembers = db.UserMembers
                    .Where(x => x.FullName == userName)
                    .Select(x => x.FullName)
                    .FirstOrDefault();

                //lastLoc = (from d in db.UserGeo
                //           where d.Id = 
                //           select new GeoLocation
                //           {
                //               UserId = d.Userid,
                //               Latitude = d.Latitude,
                //               Longitude = d.Longitude
                //           })

                //lastLoc.Add(lastLoca);
                if (!String.IsNullOrEmpty(locMembers))
                {
                    if (lastLoca != null)
                    {
                        string myDb1ConnectionString = Configuration.GetConnectionString("DevConnection");

                        using (SqlConnection connection = new SqlConnection(myDb1ConnectionString))
                        {
                            string sql = $@"Update UserMembers set latitude = '{lastLoca.Latitude}', longitude = '{lastLoca.Longitude}' 
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
        //[ValidateAntiForgeryToken]
        public IActionResult Add([FromBody]GeoLocation model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                UserGeo oGeoLoc = new UserGeo();
                oGeoLoc.Userid = model.UserId;
                oGeoLoc.Latitude = model.Latitude;
                oGeoLoc.Longitude = model.Longitude;
                oGeoLoc.DateAdded = model.DateAdded;

                db.UserGeo.Add(oGeoLoc);
                db.SaveChanges();

            }
            ModelState.Clear();
            return Content("1");
            //return View("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddPlaces([FromBody]PlacesGeoModel model)
        {
            int UserIDMembers = Convert.ToInt32(TempData["UserId"]);
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                PlacesGeo oPlacesGeo = new PlacesGeo();
                oPlacesGeo.UserId = model.UserId;
                oPlacesGeo.PlaceLat = model.PlaceLat;
                oPlacesGeo.PlaceLon = model.PlaceLon;
                oPlacesGeo.PlaceName = model.PlaceName;
                oPlacesGeo.PlaceType = model.PlaceType;
                oPlacesGeo.PlaceVicinity = model.PlaceVicinity;

                db.PlacesGeo.Add(oPlacesGeo);
                db.SaveChanges();

            }
            ModelState.Clear();
            //return Ok();
            return Content("1");
            //return View("Index");
        }
        public IActionResult UserMembers(MembersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                UserMembers oUserMembers = new UserMembers();
                oUserMembers.Id = model.Id;
                oUserMembers.UserId = model.UserId;
                oUserMembers.FullName = model.FullName;
                oUserMembers.PhoneNumber = model.PhoneNumber;
                oUserMembers.Email = model.Email;
                oUserMembers.ZipCode = model.ZipCode;

                db.UserMembers.Add(oUserMembers);
                db.SaveChanges();
            }
            TempData["UserId"] = model.UserId;
            ModelState.Clear();
            return RedirectToAction("Index");
            //return View("Index");
        }

        public IActionResult GetMembersLoc()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            List<MembersViewModel> memGeoLst = null;
            using (SimplyHelpContext db = new SimplyHelpContext())
            {
                memGeoLst = (from d in db.UserMembers
                             where d.UserId == userId
                             orderby d.Id
                             select new MembersViewModel
                             {
                                 FullName = d.FullName,
                                 Latitude = d.Latitude,
                                 Longitude = d.Longitude
                             }).ToList();

                ViewBag.ListMembGeo = memGeoLst;
            }

            return Ok(memGeoLst);
        }
    }
}
