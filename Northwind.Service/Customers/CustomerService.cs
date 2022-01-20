using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerService(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Customer> GetCustomer(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await _dbContext.Customers
                .Include(x => x.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .ToListAsync();
            return customers;
        }

        public async Task<Customer> AddCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> EditCustomer(CustomerDto customerDto)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerDto.CustomerId);
            if (customer is null)
            {
                throw new ArgumentException("Customer does not exist");
            }
            customer = _mapper.Map<Customer>(customerDto);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomer(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (customer is null)
            {
                throw new ArgumentException("Customer does not exist");
            }
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}