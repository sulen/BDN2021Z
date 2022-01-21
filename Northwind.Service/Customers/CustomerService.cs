using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class CustomerService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerService(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetCustomer(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await _dbContext.Customers
                .Include(x => x.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Include(x => x.Orders)
                .Take(10)
                .ToListAsync();
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetCustomersSingle()
        {
            var customers = await _dbContext.Customers
                .Include(x => x.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Include(x => x.Orders)
                .AsSingleQuery()
                .ToListAsync();
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetCustomersSplit()
        {
            var customers = await _dbContext.Customers
                .Include(x => x.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Include(x => x.Orders)
                .AsSplitQuery()
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
            _mapper.Map(customerDto, customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomer(string customerId)
        {
            var customer = await _dbContext.Customers
                .Include(x => x.Orders).FirstOrDefaultAsync(x => x.CustomerId == customerId);
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