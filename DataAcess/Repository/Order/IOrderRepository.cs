using BusinessObject;
using DataAcess.ViewModels;

namespace DataAcess.Repository.Order
{
    public interface IOrderRepository
    {
        public List<OrderObject> GetOrderList();
        public OrderObject GetOrder(int orderId);
        public void CreateOrder(OrderViewModel viewModel);
        public void Update(int orderId, string status);
    }
}
