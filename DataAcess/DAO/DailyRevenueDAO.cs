using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class DailyRevenueDAO
    {
        private readonly SiddleStoreDbContext _context;

        public DailyRevenueDAO(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public List<DailyRevenueObject> GetDailyRevenueList()
        {
            List<DailyRevenueObject> dailyrevenues;
            try
            {
                dailyrevenues = _context.DailyRevenues.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dailyrevenues;
        }

        public int CreateDailyRevenues()
        {
            List<OrderObject> orders = _context.Orders.ToList();
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
                        bool entryExists = _context.DailyRevenues
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
                            _context.DailyRevenues.Add(dailyRevenue);
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
                            _context.DailyRevenues.Update(dailyRevenue);
                            return _context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            return _context.SaveChanges();
        }
    }
}


