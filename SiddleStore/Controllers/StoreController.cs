using BusinessObject;
using DataAcess.Repository.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IStoreRepository storeRepository;

        public StoreController()
        {
            storeRepository = new StoreRepositoty();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetStoreList(string? search, int page = 1, int pageSize = 10) 
        {
            try
            {
                var stores = storeRepository.GetStoreList();

                if (!string.IsNullOrEmpty(search))
                {
                    stores = storeRepository.SearchStore(search, stores);
                }
                var totalCount = stores.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalCount * pageSize);

                var productsPerPage = stores
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return Ok(productsPerPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreateStore")]
        public IActionResult CreateStore(StoreObject store)
        {
            try
            {
                storeRepository.AddStore(store);
                return Ok(new { mess = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("EditStore")]
        public IActionResult EditStore(int id, StoreObject store)
        {
            try
            {
                store.StoreId = id;
                storeRepository.Update(store);
                return Ok(new { mess = "Update Store with the ID" + id + "successfully!" });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("DeleteStore")]
        public IActionResult DeleteStore(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Store ID is not found!");
                }
                storeRepository.Delete(id.Value);

                return Ok(new { mess = "Delete store successfully!" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
