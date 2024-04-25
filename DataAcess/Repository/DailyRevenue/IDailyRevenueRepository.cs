using BusinessObject;

namespace DataAcess.Repository.DailyRevenue
{
    public interface IDailyRevenueRepository
    {
        public List<DailyRevenueObject> GetDailyRevenueList();
    }
}
