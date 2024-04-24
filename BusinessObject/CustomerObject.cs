namespace BusinessObject
{
    public partial class CustomerObject
    {
        public CustomerObject() 
        {
            Orders = new HashSet<OrderObject>();
        }

        public int CustomerId { get; set; }
        public System.Nullable<int> UserId { get; set; }
        public string CustomerFullName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Balance { get; set; }

        public virtual UserObject User { get; set; } = null!;
        public virtual ICollection<OrderObject> Orders { get; }
    }
}

