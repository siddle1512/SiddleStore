namespace BusinessObject
{
    public partial class OrderDetailObject
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public decimal SubTotal { get; set; }

        public virtual OrderObject Order { get; set; } = null!;
        public virtual ProductObject Product { get; set; } = null!;
    }
}
