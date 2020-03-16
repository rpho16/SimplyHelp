using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplyHelp.Models.ViewModel
{
    public class RegistrationUserViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        //[StringLength(15, ErrorMessage = "Name length can't be more than 15.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
