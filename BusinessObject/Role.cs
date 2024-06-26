﻿namespace BusinessObject
{
    public partial class Role
    {
        public Role() 
        {
            Users = new HashSet<UserObject>();
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserObject> Users { get; }
    }
}
