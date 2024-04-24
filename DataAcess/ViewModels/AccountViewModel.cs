using BusinessObject;

namespace DataAcess.ViewModels
{
    public class AccountViewModel
    {
        public int StoreId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHashed { get; set; } = null!;
        public int CustomerId { get; set; }
    }
}
