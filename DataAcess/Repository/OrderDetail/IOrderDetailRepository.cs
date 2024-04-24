using BusinessObject;

namespace DataAcess.Repository.OrderDetail
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetailObject> GetOrderDetails(int orderId);
    }
}
