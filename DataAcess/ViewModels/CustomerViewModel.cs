namespace DataAcess.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
