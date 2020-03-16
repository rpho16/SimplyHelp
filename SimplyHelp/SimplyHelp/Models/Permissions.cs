using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
