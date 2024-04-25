using BusinessObject;

namespace DataAcess.DAO
{
    public class DailyRevenueDAO
    {
        private static DailyRevenueDAO? instance;

        public static DailyRevenueDAO Instance
        {
            get { if (instance == null) instance = new DailyRevenueDAO(); return DailyRevenueDAO.instance; }
            private set { DailyRevenueDAO.instance = value; }
        }

        public List<DailyRevenueObject> GetDailyRevenueList()
        {
            List<DailyRevenueObject> dailyrevenues;

            try
            {
                var context = new SiddleStoreDbContext();
                dailyrevenues = context.DailyRevenues.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dailyrevenues;
        }

        public void CreateDailyRevenues()
        {
            List<OrderObject> orders = OrderDAO.Instance.GetOrderList();
            var context = new SiddleStoreDbContext();
            DateTime currentDate = DateTime.Now.Date;

            if (orders != null && orders.Any())
            {
                var dailyRevenues = orders
                    .Where(o => o.Date.Date == currentDate)
                    .GroupBy(o => new { o.StoreId, o.Date.Date })
                    .Select(group => new
                    {
                        StoreId = group.Key.StoreId,
                        Date = group.Key.Date,
                        TotalRevenue = group.Sum(o => o.Total),
                        TotalOrder = group.Count(o => o.Status == "Ordered"),
                    });

                foreach (var result in dailyRevenues)
                {
                    try
                    {
                        bool entryExists = context.DailyRevenues
                            .Any(dr => dr.StoreId == result.StoreId && dr.Date == result.Date);

                        if (!entryExists)
                        {
                            DailyRevenueObject dailyRevenue = new DailyRevenueObject
                            {
                                Date = result.Date,
                                StoreId = result.StoreId,
                                TotalRevenue = result.TotalRevenue,
                                TotalOrder = result.TotalOrder
                            };
                            context.DailyRevenues.Add(dailyRevenue);
                            context.SaveChanges();
                        }
                        else
                        {
                            DailyRevenueObject dailyRevenue = new DailyRevenueObject
                            {
                                Date = result.Date,
                                StoreId = result.StoreId,
                                TotalRevenue = result.TotalRevenue,
                                TotalOrder = result.TotalOrder
                            };
                            context.DailyRevenues.Update(dailyRevenue);
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}


