using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyHelp.Models.ViewModel
{
    public class PlacesGeoModel
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
