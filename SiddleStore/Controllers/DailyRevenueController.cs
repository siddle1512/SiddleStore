using DataAcess.Repository.DailyRevenue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyRevenueController : ControllerBase
    {
        public IDailyRevenueRepository dailyRevenueRepository;
        public DailyRevenueController()
        {
            dailyRevenueRepository = new DailyRevenueRepository();
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public IActionResult GetDailyRevenueList()
        {
            try
            {
                var dailyRevenues = dailyRevenueRepository.GetDailyRevenueList();
                return Ok(dailyRevenues);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }
    }
}
