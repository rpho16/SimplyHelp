using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class UserMembers
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
