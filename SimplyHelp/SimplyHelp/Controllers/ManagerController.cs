using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyHelp.Models;
using SimplyHelp.Models.ViewModel;
//
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace SimplyHelp.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserManagerViewModel model)
        {
            ViewData["Message"] = "Your registration page!.";

            ViewBag.SuccessMessage = null;

            if (model.Email.Contains("menoth.com"))
            {
                ModelState.AddModelError("Email", "We don't support menoth Address !!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Find your Account Sid and Auth Token at twilio.com/user/account  
                    const string acctSid = "";
                    const string authT = "";
                    var accountSid = acctSid;
                    var authToken = authT;
                    TwilioClient.Init(accountSid, authToken);

                    var to = new PhoneNumber("+1" + model.PhoneNumber);
                    var message = MessageResource.Create(
                        to,
                        from: new PhoneNumber("+12064324427"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).  
                        body: $"Hello {model.Name} {model.Text}");

                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Registered Successfully !!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Registration Failure : {ex.Message} ");
                }

            }

            return View();
        }
    }
}