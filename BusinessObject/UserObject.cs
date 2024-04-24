namespace BusinessObject  
{
    public enum UserStatus
    {
        Deactivate,
        Activated,
    }
    public partial class UserObject
    {
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHashed { get; set; } = null!;
        public UserStatus Status { get; set; }

        public virtual CustomerObject? Customer { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual StoreObject Store { get; set; } = null!;
    }
}
