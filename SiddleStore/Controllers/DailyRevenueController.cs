using DataAcess.Repository.DailyRevenue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyRevenueController : ControllerBase
    {
        public IDailyRevenueRepository _dailyRevenueRepository;
        public DailyRevenueController(IDailyRevenueRepository dailyRevenueRepository)
        {
            _dailyRevenueRepository = dailyRevenueRepository;
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public IActionResult GetDailyRevenueList(int page = 1, int pageSize = 10)
        {
            try
            {
                var dailyRevenues = _dailyRevenueRepository.GetDailyRevenueList();

                var totalCount = dailyRevenues.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount * pageSize);

                var dailyRevenuesPerPage = dailyRevenues
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return Ok(dailyRevenuesPerPage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }
    }
}
