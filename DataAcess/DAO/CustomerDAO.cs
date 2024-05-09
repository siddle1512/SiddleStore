using BusinessObject;
using DataAcess.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class CustomerDAO
    {
        private readonly SiddleStoreDbContext _context;

        public CustomerDAO(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public List<CustomerObject> GetCustomerList(int? storeId = null)
        {
            List<CustomerObject> customers;
            try
            {
                if (storeId == null)
                {
                    customers = _context.Customers.Include(u => u.User).ToList();
                }
                else
                {
                    customers = _context.Customers
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
                    var _ = _context.Customers.Where(p => (p.CustomerId == customerId) || (p.NationalId.Equals(nationalId)));
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
                    searchResult = _context.Customers
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

                    _context.Customers.Add(newCustomer);
                    _context.SaveChanges();
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

        public int Update(CustomerViewModel customer)
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
                    _mem.CustomerFullName = customer.CustomerFullName;
                    _mem.CustomerPhone = customer.CustomerPhone;
                    _mem.NationalId = customer.NationalId;
                    _mem.Address = customer.Address;
                    _mem.Balance = customer.Balance;

                    _context.Customers.Update(_mem);
                    return _context.SaveChanges();
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
