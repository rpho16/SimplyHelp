using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyHelp.Models.ViewModel
{
    public class GeoLocation
    {
        public int UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string DateAdded { get; set; }
    }
}
