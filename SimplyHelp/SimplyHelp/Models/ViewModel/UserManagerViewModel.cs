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
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
