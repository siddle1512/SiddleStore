using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO? instance;

        public static OrderDetailDAO Instance
        {
            get { if (instance == null) instance = new OrderDetailDAO(); return OrderDetailDAO.instance; }
            private set { OrderDetailDAO.instance = value; }
        }

        public List<OrderDetailObject> GetOrderDetails(int orderId)
        {
            List<OrderDetailObject> orderDetails;

            try
            {
                var context = new SiddleSroteDbContext();
                orderDetails = context.OrderDetails
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
