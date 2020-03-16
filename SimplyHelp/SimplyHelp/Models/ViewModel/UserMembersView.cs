using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyHelp.Models.ViewModel
{
    public class UserMembersView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ZipCode { get; set; }
    }
}
