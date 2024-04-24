namespace BusinessObject
{
     public partial class DailyRevenueObject 
    {     
        public DateTime Date { get; set; }
        public int StoreId;
        public decimal TotalRevenue;
        public decimal TotalOrder;
        public virtual StoreObject Store { get; set; } = null!;
    }
}
