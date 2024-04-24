namespace BusinessObject
{
    public partial class OrderObject
    {
        public enum Method
        {
            Cash,
            Banking,
        }
        public OrderObject()
        {
            OrderDetails = new HashSet<OrderDetailObject>();
        }
        public int OrderId { get; set; }
        public System.Nullable<int> CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public Method PaymentMethod { get; set; }
        public string? Status { get; set; }

        public virtual CustomerObject Customer { get; set; } = null!;
        public virtual ICollection<OrderDetailObject> OrderDetails { get; }
        public StoreObject Store { get; set; } = null!;
    }
}
