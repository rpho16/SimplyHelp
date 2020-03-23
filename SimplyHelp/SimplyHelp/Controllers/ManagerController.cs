using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyHelp.Models;
using SimplyHelp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Clients;
using Twilio.Http;

namespace SimplyHelp.Controllers
{
    public class ManagerController : Controller
    {
        [BindProperty]
        public Users UserA { get; set; }

        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        private readonly SimplyHelpContext _context;

        public ManagerController(SimplyHelpContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IEnumerable<Disaster> DisasterList { get; set; }
        public IEnumerable<AlertType> AlertTypeList { get; set; }
        public IEnumerable<AlertMessage> AlertMessageList { get; set; }

        public IActionResult Index()
        {
            DisasterList = _context.Disaster.ToList();
            AlertMessageList = _context.AlertMessage.Where(d => d.IdAlertType == 1).ToList();
            ViewBag.DisasterList = DisasterList;
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserManagerViewModel model)
        {
            ViewData["Message"] = "Your registration page!.";
            ViewBag.SuccessMessage = null;
            if (ModelState.IsValid)
            {
                try
                {
                    // Find your Account Sid and Auth Token at twilio.com/user/account                      
                    var acctSid = Configuration["Twilio:AccountSid"];
                    var authT = Configuration["Twilio:AuthToken"];
                    
                    TwilioClient.Init(acctSid, authT);

                    var to = new PhoneNumber("+1" + model.PhoneNumber);
                    var message = MessageResource.Create(
                        to,
                        from: new PhoneNumber(Configuration["Twilio:ToNumber"]), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).  
                        body: $"Hello {model.Name} {model.Text}");

                    
                    ViewBag.SuccessMessage = "Registered Successfully !!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Registration Failure : {ex.Message} ");
                }
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        public JsonResult FillAlertType(int disaster)
        {
            AlertTypeList = _context.AlertType.Where(d => d.IdDisaster == disaster).ToList();
            return Json(AlertTypeList);
        }
        public JsonResult FillAlertMessage(int alertType)
        {
            var AlertMessageList = _context.AlertMessage.Where(d => d.IdAlertType == alertType).ToList();
            return Json(AlertMessageList);
        }

        [HttpPost]
        public IActionResult BulkMessage(UserManagerViewModel model)
        {
            return View();
        }
    }
}