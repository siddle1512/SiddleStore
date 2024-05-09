using BusinessObject;
using DataAcess.ViewModels;

namespace DataAcess.DAO
{
    public class OrderDAO
    {
        private readonly SiddleStoreDbContext _context;

        public OrderDAO(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public List<OrderObject> GetOrderList()
        {
            List<OrderObject> orders;
            try
            {
                orders = _context.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public OrderObject GetOrder(int orderId)
        {
            OrderObject order;
            try
            {
                order = _context.Orders
                    .Where(o => o.OrderId == orderId)
                    .AsQueryable()
                    .First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public int CreateOrder(OrderViewModel viewModel)
        {
            if (viewModel == null || viewModel.Products == null || !viewModel.Products.Any())
            {
                throw new Exception("Invalid input for creating a order!");
            }

            StoreObject store = _context.Stores.Where(s => s.StoreId == viewModel.StoreId).First();

            if (store == null)
            {
                throw new Exception("Invalid storeId!");
            }

            OrderObject order = new OrderObject();

            int ignore = viewModel.CustomerId;

            if (ignore != 0)
            {
                order.CustomerId = viewModel.CustomerId;
            }

            order.StoreId = viewModel.StoreId;
            order.Date = DateTime.Now;
            order.PaymentMethod = viewModel.PaymentMethod;
            order.Status = "Ordered";

            _context.Orders.Add(order);

            decimal total = 0;

            foreach (var productQuantity in viewModel.Products)
            {
                ProductObject product = _context.Products.Where(p => p.ProductId == productQuantity.ProductId).First();

                if (product == null)
                {
                    throw new Exception("Invalid productId!");
                }

                OrderDetailObject orderDetail = new OrderDetailObject();

                orderDetail.OrderId = order.OrderId;
                orderDetail.ProductId = productQuantity.ProductId;
                orderDetail.Quantity = productQuantity.Quantity;
                orderDetail.SubTotal = (product.Price - (product.Price * (decimal)product.Discount)) * productQuantity.Quantity;
                orderDetail.Discount = product.Discount;

                total += orderDetail.SubTotal;

                product.InStock -= productQuantity.Quantity;

                if (product.InStock < 0)
                {
                    throw new Exception(product.ProductName + "is out of stock!");
                }

                _context.Products.Update(product);

                _context.OrderDetails.Add(orderDetail);
            }

            order.Total = total;
            return _context.SaveChanges();
        }

        public void Update(int orderId, string status)
        {
            if (status == null)
            {
                throw new Exception("Status is undefined!");
            }
            try
            {
                OrderObject order = GetOrder(orderId);
                if (order != null)
                {
                    order.Status = status;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
