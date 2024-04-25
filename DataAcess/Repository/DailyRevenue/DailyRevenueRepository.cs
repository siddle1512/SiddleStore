using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.DailyRevenue
{
    public class DailyRevenueRepository : IDailyRevenueRepository
    {
        public List<DailyRevenueObject> GetDailyRevenueList() => DailyRevenueDAO.Instance.GetDailyRevenueList();
    }
}
