using Microsoft.Extensions.DependencyInjection;

namespace Northwind.Service
{
    public static class ServiceModule
    {
        public static IServiceCollection AddServiceModule(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(ServiceModule));

            service.AddTransient<ICustomerService, CustomerService>();
            service.AddTransient<IOrderService, OrderService>();

            return service;
        }
    }
}