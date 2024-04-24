using BusinessObject;

namespace DataAcess.Repository.Store
{
    public interface IStoreRepository
    {
        public List<StoreObject> GetStoreList();
        public List<StoreObject> SearchStore(string name, List<StoreObject> searchList);
        public StoreObject GetStore(int storeId, List<StoreObject>? searchList = null);
        public void AddStore(StoreObject store);
        public void Update(StoreObject store);
        public void Delete(int storeId);

    }
}
