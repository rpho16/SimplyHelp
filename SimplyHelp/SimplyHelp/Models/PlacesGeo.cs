using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class PlacesGeo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceVicinity { get; set; }
        public string PlaceType { get; set; }
        public string PlaceLat { get; set; }
        public string PlaceLon { get; set; }
    }
}
