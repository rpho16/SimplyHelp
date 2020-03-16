using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public DateTime? Date { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
    }
}
