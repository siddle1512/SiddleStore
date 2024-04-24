using BusinessObject;
using DataAcess.ViewModels;

namespace DataAcess.Repository.Customer
{
    public interface ICustomerRepository
    {
        public List<CustomerObject> GetCustomerList(int? storeId = null);
        public List<CustomerObject> SearchCustomer(string name, List<CustomerObject> searchList);
        public void CreateAccount(CustomerViewModel customer);
        public void Update(CustomerViewModel customer);
    }
}
