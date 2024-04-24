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
                var context = new SiddleSroteDbContext();
                dailyrevenues = context.DailyRevenues.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dailyrevenues;
        }
    }
}
