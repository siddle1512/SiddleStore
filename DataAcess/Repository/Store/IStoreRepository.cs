using BusinessObject;

namespace DataAcess.Repository.Store
{
    public interface IStoreRepository
    {
        public Task<List<StoreObject>> GetStoreList();
        public Task<List<StoreObject>> SearchStore(string name, List<StoreObject> searchList);
        public Task<StoreObject> GetStore(int storeId, List<StoreObject>? searchList = null);
        public Task<bool> AddStore(StoreObject store);
        public Task<bool> Update(StoreObject store);
        public Task<bool> Delete(int storeId);
    }
}
