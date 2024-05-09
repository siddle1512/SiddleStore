using BusinessObject;
using DataAcess.DAO;
using DataAcess.ViewModels;

namespace DataAcess.Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDAO _orderDAO;

        public OrderRepository(OrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }

        public OrderObject GetOrder(int orderId) => _orderDAO.GetOrder(orderId);
        public List<OrderObject> GetOrderList() => _orderDAO.GetOrderList();
        public void CreateOrder(OrderViewModel viewModel) => _orderDAO.CreateOrder(viewModel);
        public void Update(int orderId, string status) => _orderDAO.Update(orderId, status);
    }
}
