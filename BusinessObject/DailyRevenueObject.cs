namespace BusinessObject
{
     public partial class DailyRevenueObject 
    {     
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalOrder { get; set; }
        public virtual StoreObject Store { get; set; } = null!;
    }
}
