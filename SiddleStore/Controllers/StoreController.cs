using BusinessObject;
using DataAcess.Repository.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiddleStore.ExceptionFilterHandeling;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("All", Name = "GetStoreList")]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> GetStoreList(string? search, int page = 1, int pageSize = 10)
        {
            var stores = await _storeRepository.GetStoreList();

            if (!string.IsNullOrEmpty(search))
            {
                stores = await _storeRepository.SearchStore(search, stores);
            }
            var totalCount = stores.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount * pageSize);

            var productsPerPage = stores
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return Ok(productsPerPage);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] StoreObject store)
        {
            await _storeRepository.AddStore(store);
            return Ok(new { mess = "" });
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id:int}")]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> EditStore(int id, [FromBody] StoreObject store)
        {
            store.StoreId = id;
            bool result = await _storeRepository.Update(store);
            return Ok(new { mess = "Update Store with the ID" + id + "successfully!" });
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:int}")]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _storeRepository.Delete(id);
            return Ok(new { mess = "Delete store successfully!" });
        }
    }
}
