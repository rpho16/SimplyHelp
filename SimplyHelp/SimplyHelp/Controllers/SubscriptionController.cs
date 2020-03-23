using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyHelp.Models;
using SimplyHelp.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
//Twilio
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace SimplyHelp.Controllers
{
    public class SubscriptionController : Controller
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;
        private readonly SimplyHelpContext _context;

        [BindProperty]
        public TblSubscription Subscription { get; set; }

        public SubscriptionController(SimplyHelpContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

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
        public IActionResult Add(TblSubscription model)
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

            Subscription = new TblSubscription();
            _context.TblSubscription.Add(model);
            _context.SaveChanges();

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