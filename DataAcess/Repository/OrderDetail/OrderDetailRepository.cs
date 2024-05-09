using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.OrderDetail
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDetailDAO _orderDetailDAO;

        public OrderDetailRepository(OrderDetailDAO orderDetailDAO)
        {
            _orderDetailDAO = orderDetailDAO;
        }

        public List<OrderDetailObject> GetOrderDetails(int orderId) => _orderDetailDAO.GetOrderDetails(orderId);
    }
}
