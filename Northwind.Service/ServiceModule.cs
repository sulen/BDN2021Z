using Microsoft.Extensions.DependencyInjection;

namespace Northwind.Service
{
    public static class ServiceModule
    {
        public static IServiceCollection AddServiceModule(this IServiceCollection service)
        {
            service.AddTransient<CustomerService>();
            service.AddTransient<OrderService>();
            service.AddTransient<ProductService>();

            return service;
        }
    }
}