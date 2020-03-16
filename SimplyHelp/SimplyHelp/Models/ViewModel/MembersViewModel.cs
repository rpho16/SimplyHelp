using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimplyHelp.Models.ViewModel
{
    public class MembersViewModel
    {        
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
    }
}
