using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class OrderDetailDAO
    {
        private readonly SiddleStoreDbContext _context;

        public OrderDetailDAO(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public List<OrderDetailObject> GetOrderDetails(int orderId)
        {
            List<OrderDetailObject> orderDetails;

            try
            {
                orderDetails = _context.OrderDetails
                    .Where(o => o.OrderId == orderId)
                    .Include(p => p.Product)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }
    }
}
