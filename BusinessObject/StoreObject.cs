namespace BusinessObject
{
    public enum StoreStatus
    {
        Enable,
        Disable,
    }
    public partial class StoreObject
    {
        public StoreObject()
        {
            Users = new HashSet<UserObject>();
            DailyRevenues = new HashSet<DailyRevenueObject>();
            Orders = new HashSet<OrderObject>();
        }
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public virtual StoreStatus Status { get; set; }
        public virtual ICollection<UserObject> Users { get; }
        public virtual ICollection<DailyRevenueObject> DailyRevenues { get; }
        public virtual ICollection<OrderObject> Orders { get; }
    }
}
