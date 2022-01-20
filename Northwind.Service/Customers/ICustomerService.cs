using Northwind.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(string customerId);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> AddCustomer(CustomerDto customerDto);
        Task<Customer> EditCustomer(CustomerDto customerDto);
        Task<Customer> DeleteCustomer(string customerId);
    }
}