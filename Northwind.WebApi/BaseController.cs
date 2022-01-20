using Microsoft.AspNetCore.Mvc;

namespace Northwind.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
