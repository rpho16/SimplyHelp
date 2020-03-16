using System;
using System.Collections.Generic;

namespace SimplyHelp.Models
{
    public partial class Role
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
