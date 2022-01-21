using Microsoft.AspNetCore.Mvc;
using Northwind.Service;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public async Task<ActionResult> GetProducts([FromQuery] ProductQueryDto productQueryDto)
        {
            var result = await _productService.GetFilteredProducts(productQueryDto);
            return Ok(result);
        }

        [HttpGet("sales")]
        public async Task<ActionResult> GetProductSalesByQuarter()
        {
            var result = await _productService.GetQuarterProductSales();
            return Ok(result);
        }
    }
}
