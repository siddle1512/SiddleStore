using BusinessObject;
using DataAcess.DAO;
using DataAcess.ViewModels;

namespace DataAcess.Repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO _customerDAO;

        public CustomerRepository(CustomerDAO customerDAO)
        {
            _customerDAO = customerDAO;
        }

        public List<CustomerObject> GetCustomerList(int? storeId = null) => _customerDAO.GetCustomerList(storeId);
        public List<CustomerObject> SearchCustomer(string name, List<CustomerObject> searchList) => _customerDAO.SearchCustomer(name, searchList);
        public void CreateAccount(CustomerViewModel customer) => _customerDAO.CreateAccount(customer);
        public void Update(CustomerViewModel customer) => _customerDAO.Update(customer);

    }
}
