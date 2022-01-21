using AutoMapper;
using Northwind.Domain;
using Northwind.Service;

namespace Northwind.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<OrderDto, Order>();

        }
    }
}