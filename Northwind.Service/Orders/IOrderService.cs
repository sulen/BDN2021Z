using Northwind.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersSplitQuery(string customerId);
        Task<Order> AddOrder(OrderDto orderDto);
    }
}