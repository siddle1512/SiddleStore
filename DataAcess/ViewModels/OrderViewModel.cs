using static BusinessObject.OrderObject;

namespace DataAcess.ViewModels
{
    public class ProductQuantityViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderViewModel
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public Method PaymentMethod { get; set; }
        public List<ProductQuantityViewModel>? Products { get; set; }
    }
}
