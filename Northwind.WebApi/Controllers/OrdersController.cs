using Microsoft.AspNetCore.Mvc;
using Northwind.Service;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetOrders(string customerId)
        {
            var result = await _orderService.GetOrdersSplitQuery(customerId);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            var result = await _orderService.AddOrder(orderDto);
            return Ok(result);
        }

        [HttpPost("copy")]
        public async Task<ActionResult> CopyOrder(short orderId)
        {
            var result = await _orderService.CopyOrder(orderId);
            return Ok(result);
        }
    }
}