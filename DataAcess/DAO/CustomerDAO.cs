using BusinessObject;
using DataAcess.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO? instance;

        public static CustomerDAO Instance
        {
            get { if (instance == null) instance = new CustomerDAO(); return CustomerDAO.instance; }
            private set { CustomerDAO.instance = value; }
        }

        public List<CustomerObject> GetCustomerList(int? storeId = null)
        {
            List<CustomerObject> customers;
            var context = new SiddleSroteDbContext();
            try
            {
                if (storeId == null)
                {
                    customers = context.Customers.Include(u => u.User).ToList();
                }
                else
                {
                    customers = context.Customers
                        .Where(c => c.User.StoreId == storeId)
                        .Include(u => u.User)
                        .ToList();
                }               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public CustomerObject GetCustomer(int? customerId, string? nationalId, List<CustomerObject>? searchList = null)
        {
            CustomerObject customer = null!;

            try
            {
                if (searchList == null)
                {
                    var context = new SiddleSroteDbContext();
                    var _ = context.Customers.Where(p => (p.CustomerId == customerId) || (p.NationalId.Equals(nationalId)));
                    if (_ != null && _.Any())
                    {
                        customer = _.First();
                    }
                }
                else
                {
                    customer = searchList.Where(p => (p.CustomerId == customerId) || (p.NationalId.Equals(nationalId)))
                        .AsQueryable()
                        .First();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }

        public List<CustomerObject> SearchCustomer(string name, List<CustomerObject> searchList)
        {
            List<CustomerObject> searchResult;
            try
            {
                if (searchList == null)
                {
                    var context = new SiddleSroteDbContext();
                    searchResult = context.Customers
                                        .Where(p => p.CustomerFullName.ToLower().Contains(name.ToLower()))
                                        .ToList();
                }
                else
                {
                    searchResult = searchList.Where(p => p.CustomerFullName.ToLower().Contains(name.ToLower())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return searchResult;
        }

        public void CreateAccount(CustomerViewModel customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new Exception("Customer is undefined!!");
                }
                CustomerObject newCustomer = new CustomerObject();

                if(GetCustomer(null, customer.NationalId) == null)
                {
                    newCustomer.CustomerFullName = customer.CustomerFullName;
                    newCustomer.CustomerPhone = customer.CustomerPhone;
                    newCustomer.NationalId = customer.NationalId;
                    newCustomer.Address = customer.Address;
                    newCustomer.Balance = customer.Balance;

                    var context = new SiddleSroteDbContext();

                    context.Customers.Add(newCustomer);                  
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer is existed!");
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CustomerViewModel customer)
        {
            if (customer == null)
            {
                throw new Exception("Customer is undefined!!");
            }
            try
            {
                CustomerObject _mem = GetCustomer(customer.CustomerId, null);
                if (_mem != null)
                {
                    var context = new SiddleSroteDbContext();

                    _mem.CustomerFullName = customer.CustomerFullName;
                    _mem.CustomerPhone = customer.CustomerPhone;
                    _mem.NationalId = customer.NationalId;
                    _mem.Address = customer.Address;
                    _mem.Balance = customer.Balance;

                    context.Customers.Update(_mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
