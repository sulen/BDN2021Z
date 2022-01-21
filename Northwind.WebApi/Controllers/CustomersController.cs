using Microsoft.AspNetCore.Mvc;
using Northwind.Service;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(string id)
        {
            var result = await _customerService.GetCustomer(id);
            return result is null 
                ? NotFound() : 
                Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetCustomers()
        {
            var result = await _customerService.GetCustomers();
            return result is null
                ? NotFound() :
                Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult> AddCustomer(CustomerDto customerDto)
        {
            var result = await _customerService.AddCustomer(customerDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCustomer(string id, [FromBody] CustomerDto customerDto)
        {
            var result = await _customerService.EditCustomer(customerDto with { CustomerId = id });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}