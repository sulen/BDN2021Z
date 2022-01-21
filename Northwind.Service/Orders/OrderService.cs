using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class OrderService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Category)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Supplier)
                .Include(x => x.Customer.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Take(50)
                .ToListAsync();
            return orders;
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
                .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrders(string customerId)
        {
            var orders = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Category)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product.Supplier)
                .Include(x => x.Customer.CustomerCustomerDemos)
                    .ThenInclude(x => x.CustomerType)
                .Where(x => x.CustomerId == customerId)
                .Take(50)
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
                .ToListAsync();
            return orders;
        }

        public async Task<Order> AddOrder(OrderDto orderDto)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            var id = await _dbContext.Orders.Select(x => x.OrderId).MaxAsync();
            var order = _mapper.Map<Order>(orderDto);
            order.OrderId = (short)(id + 1);
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
            transaction.Commit();

            return order;
        }

        public async Task<Order> CopyOrder(short orderId)
        {
            var order = await _dbContext.Orders
                .Include(x => x.OrderDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
            var id = await _dbContext.Orders.Select(x => x.OrderId).MaxAsync();
            order.OrderId = (short)(id + 1);
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.OrderId = order.OrderId;
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}