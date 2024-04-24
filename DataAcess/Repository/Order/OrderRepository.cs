using BusinessObject;
using DataAcess.DAO;
using DataAcess.ViewModels;

namespace DataAcess.Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        public OrderObject GetOrder(int orderId) => OrderDAO.Instance.GetOrder(orderId);
        public List<OrderObject> GetOrderList() => OrderDAO.Instance.GetOrderList();
        public void CreateOrder(OrderViewModel viewModel) => OrderDAO.Instance.CreateOrder(viewModel);
        public void Update(int orderId, string status) => OrderDAO.Instance.Update(orderId, status);
    }
}
