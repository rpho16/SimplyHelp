using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyHelp.Models.TableViewModel
{
    public class MembersTableViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
    }
}
