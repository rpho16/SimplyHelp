using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyHelp.Models.ViewModel
{
    public class UserManagerViewModel
    {
        [Key]
        [StringLength(100, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(130, ErrorMessage = "The {0} needs to be less than {1} characters", MinimumLength = 1)]
        [Display(Name = "Message To Send")]
        public string Text { get; set; }
        [Display(Name = "Disaster")]
        public string Disaster { get; set; }
        [Display(Name = "Alert Type")]
        public string AlertType { get; set; }
        [Display(Name = "Alert Message")]
        public string AlertMessage { get; set; }
    }
}
