using DataAcess.Repository.Customer;
using DataAcess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository customerRepository;

        public CustomerController()
        {
            customerRepository = new CustomerRepository();
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public IActionResult GetCustomerList(string? search, int page = 1, int pageSize = 10, int? storeid = null)
        {
            try
            {
                var customers = customerRepository.GetCustomerList(storeid).ToList();

                if (!string.IsNullOrEmpty(search))
                {
                    customers = customerRepository.SearchCustomer(search, customers);
                }

                var totalCount = customers.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalCount * pageSize);

                var productsPerPage = customers
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return Ok(productsPerPage);
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }

        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(CustomerViewModel customer)
        {
            try
            {
                customerRepository.CreateAccount(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new { mess = "Create a customer successfully!" });
        }


        [Authorize(Roles = "Manager, Employee")]
        [HttpPut("EditCustomer")]
        public IActionResult EditCustomer(int id, CustomerViewModel customer)
        {
            try
            {
                customer.CustomerId = id;
                customerRepository.Update(customer);             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new { mess = "Update Customer with the ID" + id + "successfully!" });
        }
    }
}
