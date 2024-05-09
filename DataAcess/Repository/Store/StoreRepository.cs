using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Repository.Store
{
    public class StoreRepository : IStoreRepository
    {
        public readonly SiddleStoreDbContext _context;
        public StoreRepository(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddStore(StoreObject store)
        {
            if (store == null)
            {
                throw new Exception("Store is undefined!");
            }
            if (GetStore(store.StoreId) != null)
            {
                _context.Stores.Add(store);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new InvalidOperationException("Store is existed!");
            }
        }

        public async Task<bool> Delete(int storeId)
        {
            StoreObject store = await GetStore(storeId);
            if (store != null)
            {
                _context.Stores.Remove(store);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new InvalidOperationException("Store does not exist!");
            }
        }

        public async Task<StoreObject> GetStore(int storeId, List<StoreObject>? searchList = null)
        {
            StoreObject store = null!;

            if (searchList == null)
            {
                var result = _context.Stores.AsNoTracking().Where(s => s.StoreId == storeId);
                if (result != null &&  result.Any())
                {
                    store = await result.FirstAsync();
                }
            }
            else
            {
                store = await searchList.Where(s => s.StoreId == storeId)
                                  .AsQueryable()
                                  .FirstAsync();
            }
            return store;
        }

        public async Task<List<StoreObject>> GetStoreList()
        {
            List<StoreObject> stores;
            stores = await _context.Stores.Include(u => u.Users).ToListAsync();
            return stores;
        }

        public async Task<List<StoreObject>> SearchStore(string name, List<StoreObject> searchList)
        {
            List<StoreObject> searchResult;
            if (searchList == null)
            {
                searchResult = await _context.Stores.Where(p => p.StoreName.ToLower().Contains(name.ToLower()))
                                                    .Include(u => u.Users)
                                                    .ToListAsync();
            }
            else
            {
                searchResult = await searchList.Where(p => p.StoreName.ToLower().Contains(name.ToLower()))
                                               .AsQueryable()
                                               .Include(u => u.Users)
                                               .ToListAsync();
            }
            return searchResult;
        }

        public async Task<bool> Update(StoreObject store)
        {
            if (store == null)
            {
                throw new InvalidOperationException("Store is undefined!");
            }
            StoreObject _mem = await GetStore(store.StoreId);
            if (_mem != null)
            {
                _context.Stores.Update(store);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new InvalidOperationException("Store does not exist!");
            }
        }
    }
}

