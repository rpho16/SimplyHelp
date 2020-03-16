using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyHelp.Models.ViewModel;
using Microsoft.AspNetCore.Http;
//Twilio
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace SimplyHelp.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {            
            return View();            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SubscriptionViewModel model)
        {
            ViewData["Message"] = "Your registration page!.";

            ViewBag.SuccessMessage = null;

            if (model.EmailAddress.Contains("menoth.com"))
            {
                ModelState.AddModelError("Email", "We don't support menoth Address !!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Models.SimplyHelpContext db = new Models.SimplyHelpContext())
            {
                Models.TblSubscription oSubscription = new Models.TblSubscription();
                oSubscription.FirstName = model.FirstName;
                oSubscription.LastName = model.LastName;
                oSubscription.Address1 = model.Address1;
                oSubscription.Address2 = model.Address2;
                oSubscription.City = model.City;
                oSubscription.State = model.State;
                oSubscription.ZipCode = model.ZipCode;
                oSubscription.County = model.County;
                oSubscription.EmailAddress = model.EmailAddress;
                oSubscription.PhoneNumber = model.PhoneNumber;
                oSubscription.AuthorizeContact = model.AuthorizeContact;

                db.TblSubscription.Add(oSubscription);
                db.SaveChanges();                
            }
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
                    body: $"Hello {model.FirstName} {model.LastName} Thanks for subscribing with Us. More info please go to URL");
                
                ViewBag.SuccessMessage = "Registered Successfully !!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Registration Failure : {ex.Message} ");
            }
            ModelState.Clear();
            return View();
        }
    }
}