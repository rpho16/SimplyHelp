using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class AlertMessage
    {
        public int Id { get; set; }
        public string AlertMessageName { get; set; }
        public int IdAlertType { get; set; }
        public virtual AlertType IdAlertTypeNavigation { get; set; }
    }
}
