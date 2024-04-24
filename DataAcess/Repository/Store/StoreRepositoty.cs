using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.Store
{
    public class StoreRepositoty : IStoreRepository
    {
        public void AddStore(StoreObject store) => StoreDAO.Instance.AddStore(store);
        public void Delete(int storeId) => StoreDAO.Instance.Delete(storeId);
        public StoreObject GetStore(int storeId, List<StoreObject>? searchList = null) => StoreDAO.Instance.GetStore(storeId, searchList);
        public List<StoreObject> GetStoreList() => StoreDAO.Instance.GetStoreList();
        public List<StoreObject> SearchStore(string name, List<StoreObject> searchList) => StoreDAO.Instance.SearchStore(name, searchList);
        public void Update(StoreObject store) => StoreDAO.Instance.Update(store);
    }
}
