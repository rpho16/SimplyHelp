using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class Disaster
    {
        public Disaster()
        {
            AlertType = new HashSet<AlertType>();
        }
        public int Id { get; set; }
        public string DisasterName { get; set; }
        public virtual ICollection<AlertType> AlertType { get; set; }
    }
}
