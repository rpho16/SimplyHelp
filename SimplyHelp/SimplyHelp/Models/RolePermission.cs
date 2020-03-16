using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int? IdRole { get; set; }
        public int? IdPermission { get; set; }

        public virtual Permissions IdPermissionNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; }
    }
}
