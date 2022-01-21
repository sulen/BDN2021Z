using AutoMapper;
using BenchmarkDotNet.Attributes;
using Moq;
using Northwind.Domain;
using Northwind.Service;
using System;
using System.Threading.Tasks;

namespace Benchmark
{
    public class QueryBenchmark
    {
        public readonly DatabaseContext _dbContext;
        public readonly IMapper _mapper;

        public QueryBenchmark()
        {
            var contextFactory = new DatabaseContextFactory();
            _dbContext = contextFactory.CreateDbContext(Array.Empty<string>());
            _mapper = new Mock<IMapper>().Object;
        }

        [Benchmark]
        public async Task GetOrdersSingleQuery()
        {
            var ordersService = new OrderService(_dbContext, _mapper);
            await ordersService.GetOrdersSingleQuery("ANATR");
        }

        [Benchmark]
        public async Task GetOrdersSplitQuery()
        {
            var ordersService = new OrderService(_dbContext, _mapper);
            await ordersService.GetOrdersSplitQuery("ANATR");
        }

        [Benchmark]
        public async Task GetAllOrdersSingleQuery()
        {
            var ordersService = new OrderService(_dbContext, _mapper);
            await ordersService.GetAllOrdersSingleQuery();
        }

        [Benchmark]
        public async Task GetAllOrdersSplitQuery()
        {
            var ordersService = new OrderService(_dbContext, _mapper);
            await ordersService.GetAllOrdersSplitQuery();
        }

        [Benchmark]
        public async Task GetProductsQuery()
        {
            var ordersService = new ProductService(_dbContext);
            await ordersService.GetFilteredProducts(new ProductQueryDto
            {
                CategoryDescription = "5623fsdf3523",
                CompanyName = "ANTR",
                ProductName = "PRODUCT",
                GlobalFilterTerm = "a"
            });
        }

        [Benchmark]
        public async Task GetQuarterProductSales()
        {
            var ordersService = new ProductService(_dbContext);
            await ordersService.GetQuarterProductSales();
        }

        [Benchmark]
        public async Task GetQuarterProductSalesLinq()
        {
            var ordersService = new ProductService(_dbContext);
            await ordersService.GetQuarterProductSalesLinq();
        }

        [Benchmark]
        public async Task GetCustomersSingle()
        {
            var ordersService = new CustomerService(_dbContext, _mapper);
            await ordersService.GetCustomersSingle();
        }

        [Benchmark]
        public async Task GetCustomersSplit()
        {
            var ordersService = new CustomerService(_dbContext, _mapper);
            await ordersService.GetCustomersSplit();
        }
    }
}