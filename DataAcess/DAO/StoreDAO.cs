using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class StoreDAO
    {
        private static StoreDAO? instance;

        public static StoreDAO Instance
        {
            get { if (instance == null) instance = new StoreDAO(); return StoreDAO.instance; }
            private set { StoreDAO.instance = value; }
        }
        public List<StoreObject> GetStoreList()
        {
            List<StoreObject> stores;

            try
            {
                var context = new SiddleStoreDbContext();
                stores = context.Stores
                    .Include(u => u.Users)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stores;
        }

        public StoreObject GetStore(int storeId, List<StoreObject>? searchList = null)
        {
            StoreObject store = null!;

            try
            {
                if (searchList == null)
                {
                    var context = new SiddleStoreDbContext();
                    var _ = context.Stores.Where(s => s.StoreId == storeId);
                    if (_ != null && _.Any())
                    {
                        store = _.First();
                    }
                }
                else
                {
                    store = searchList.Where(s => s.StoreId == storeId)
                        .AsQueryable()
                        .First();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return store;
        }

        public List<StoreObject> SearchStore(string name, List<StoreObject> searchList)
        {
            List<StoreObject> searchResult;

            try
            {
                if (searchList == null)
                {
                    var context = new SiddleStoreDbContext();
                    searchResult = context.Stores
                                        .Where(p => p.StoreName.ToLower().Contains(name.ToLower()))
                                        .Include(u => u.Users)
                                        .ToList();
                }
                else
                {
                    searchResult = searchList.Where(p => p.StoreName.ToLower().Contains(name.ToLower()))
                        .AsQueryable()
                        .Include(u => u.Users)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return searchResult;
        }

        public void AddStore(StoreObject store)
        {
            if (store == null)
            {
                throw new Exception("Store is undefined!");
            }
            try
            {
                if (GetStore(store.StoreId) == null)
                {
                    var context = new SiddleStoreDbContext();
                    context.Stores.Add(store);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Store is existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(StoreObject store)
        {
            if (store == null)
            {
                throw new Exception("Store is undefined!");
            }
            try
            {
                StoreObject _mem = GetStore(store.StoreId);
                if (_mem != null)
                {
                    var context = new SiddleStoreDbContext();
                    context.Stores.Update(store);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Store does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int storeId)
        {
            try
            {
                StoreObject store = GetStore(storeId);
                if (store != null)
                {
                    var context = new SiddleStoreDbContext();
                    context.Stores.Remove(store);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Store does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
