namespace BusinessObject
{
    public enum ProductStatus
    { 
        Available,
        SoldOut,
    }
    public partial class ProductObject
    {
        public ProductObject()
        {
            OrderDetails = new HashSet<OrderDetailObject>();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public int InStock { get; set; }
        public ProductStatus Status { get; set; }

        public virtual ICollection<OrderDetailObject> OrderDetails { get; }
    }
}
