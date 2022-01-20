using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersSplitQuery()
        {
            var orders = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Category)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Supplier)
                .Include(x => x.Customer.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .AsSplitQuery()
                //.AsNoTracking()
                .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersSingleQuery()
        {
            var orders = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Category)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Supplier)
                .Include(x => x.Customer.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .AsSingleQuery()
                //.AsNoTracking()
                .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersSplitQuery(string customerId)
        {
            var orders = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Category)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Supplier)
                .Include(x => x.Customer.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Where(x => x.CustomerId == customerId)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersSingleQuery(string customerId)
        {
            var orders = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Category)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Supplier)
                .Include(x => x.Customer.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Where(x => x.CustomerId == customerId)
                .AsSingleQuery()
                .AsNoTracking()
                .ToListAsync();
            return orders;
        }

        public async Task<Order> AddOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            foreach (var product in orderDto.Products)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                });
            }
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}