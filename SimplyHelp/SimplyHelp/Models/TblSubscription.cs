using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class TblSubscription
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public bool? AuthorizeContact { get; set; }
    }
}
