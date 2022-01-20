using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Moq;
using Northwind.Domain;
using Northwind.Service;
using System;
using System.Threading.Tasks;

namespace Benchmark
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .AddValidator(JitOptimizationsValidator.DontFailOnError)
                .AddLogger(ConsoleLogger.Default)
                .AddColumnProvider(DefaultColumnProviders.Instance);
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly, config);
        }
    }

    public class OrdersBenchmark
    {
        public readonly DatabaseContext _dbContext;
        public readonly IMapper _mapper;

        public OrdersBenchmark()
        {
            var contextFactory = new DatabaseContextFactory();
            _dbContext = contextFactory.CreateDbContext(Array.Empty<string>());
            _mapper = new Mock<IMapper>().Object;
        }

        [Benchmark]
        public async Task GetOrdersQuery()
        {
            var ordersService = new OrderService(_dbContext, _mapper);
            await ordersService.GetOrders("ANATR");
        }
    }
}