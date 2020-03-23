using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class AlertType
    {
        public AlertType()
        {
            AlertMessage = new HashSet<AlertMessage>();
        }
        public int Id { get; set; }
        public string AlertTypeName { get; set; }
        public int IdDisaster { get; set; }
        public virtual Disaster IdDisasterNavigation { get; set; }
        public virtual ICollection<AlertMessage> AlertMessage { get; set; }
    }
}
