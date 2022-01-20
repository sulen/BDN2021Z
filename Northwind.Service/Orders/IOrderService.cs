using Northwind.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders(string customerId);
        Task<Order> AddOrder(OrderDto orderDto);
    }
}