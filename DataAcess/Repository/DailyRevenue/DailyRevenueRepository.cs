using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.DailyRevenue
{
    public class DailyRevenueRepository : IDailyRevenueRepository
    {
        private readonly DailyRevenueDAO _dailyRevenueDAO;

        public DailyRevenueRepository(DailyRevenueDAO dailyRevenueDAO)
        {
            _dailyRevenueDAO = dailyRevenueDAO;
        }

        public List<DailyRevenueObject> GetDailyRevenueList() => _dailyRevenueDAO.GetDailyRevenueList();
        public int CreateDailyRevenues() => _dailyRevenueDAO.CreateDailyRevenues();
    }
}
