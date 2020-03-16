using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SimplyHelp.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
