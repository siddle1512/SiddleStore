using BusinessObject;
using DataAcess.DAO;
using DataAcess.ViewModels;

namespace DataAcess.Repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<CustomerObject> GetCustomerList(int? storeId = null) => CustomerDAO.Instance.GetCustomerList(storeId);
        public List<CustomerObject> SearchCustomer(string name, List<CustomerObject> searchList) => CustomerDAO.Instance.SearchCustomer(name, searchList);
        public void CreateAccount(CustomerViewModel customer) => CustomerDAO.Instance.CreateAccount(customer);
        public void Update(CustomerViewModel customer) => CustomerDAO.Instance.Update(customer);

    }
}
