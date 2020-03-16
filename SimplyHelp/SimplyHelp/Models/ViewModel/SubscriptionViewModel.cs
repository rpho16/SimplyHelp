using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SimplyHelp.Models.ViewModel
{
    public class SubscriptionViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string EmailAddress { get; set; }

        [Required]        
        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string Address1 { get; set; }
        
        [Display(Name = "Address 2")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string Address2 { get; set; }

        [Required]        
        [Display(Name = "City")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string State { get; set; }

        [Required]
        [Display(Name = "County")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string County { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string ZipCode { get; set; }        

        [Required]
        public bool AuthorizeContact { get; set; }
    }
}
