using Microsoft.AspNetCore.Mvc;
using Northwind.Service;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetOrders(string customerId)
        {
            var result = await _orderService.GetOrdersSplitQuery(customerId);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            var result = await _orderService.AddOrder(orderDto);
            return Ok(result);
        }
    }
}