using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class UserGeo
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string DateAdded { get; set; }
    }
}
