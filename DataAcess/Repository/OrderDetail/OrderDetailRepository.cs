using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.OrderDetail
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public List<OrderDetailObject> GetOrderDetails(int orderId) => OrderDetailDAO.Instance.GetOrderDetails(orderId);
    }
}
