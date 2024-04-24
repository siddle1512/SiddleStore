using BusinessObject;
using DataAcess.ViewModels;

namespace DataAcess.DAO
{
    public class OrderDAO
    {
        private static OrderDAO? instance;

        public static OrderDAO Instance
        {
            get { if (instance == null) instance = new OrderDAO(); return OrderDAO.instance; }
            private set { OrderDAO.instance = value; }
        }

        public List<OrderObject> GetOrderList()
        {
            List<OrderObject> orders;
            try
            {
                var context = new SiddleSroteDbContext();
                orders = context.Orders.ToList();
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
                var context = new SiddleSroteDbContext();
                order = context.Orders
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

        public void CreateOrder(OrderViewModel viewModel)
        {
            if (viewModel == null || viewModel.Products == null || !viewModel.Products.Any())
            {
                throw new Exception("Invalid input for creating a order!");
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

            var context = new SiddleSroteDbContext();
            context.Orders.Add(order);

            context.SaveChanges();

            decimal total = 0;

            foreach (var productQuantity in viewModel.Products)
            {
                ProductObject product = ProductDAO.Instance.GetProduct(productQuantity.ProductId);

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

                context.Products.Update(product);

                context.OrderDetails.Add(orderDetail);
            }

            order.Total = total;
            context.SaveChanges();
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
                    var context = new SiddleSroteDbContext();

                    order.Status = status;
                    context.Orders.Update(order);
                    context.SaveChanges();
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
