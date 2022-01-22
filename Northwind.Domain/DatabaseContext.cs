using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Northwind.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public DbSet<QuarterProductSales> QuarterProductSales { get; set; }

        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CustomerCustomerDemo>()
                .HasOne(c => c.Customer)
                .WithMany(x => x.CustomerCustomerDemos)
                .HasForeignKey(x => x.CustomerId);

            builder.Entity<OrderDetail>()
                .HasOne(c => c.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId);

            builder.Entity<OrderDetail>()
                .HasOne(c => c.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId);

            builder.Entity<OrderDetail>()
                .HasKey(x => new { x.OrderId, x.ProductId });
         
            builder.Entity<Shipper>()
                .HasMany(c => c.Orders)
                .WithOne(x => x.ShipViaNavigation)
                .HasForeignKey(x => x.ShipVia);

            builder.Entity<Order>()
                .Property(p => p.ShippedDate)
                .IsConcurrencyToken();

            builder.Entity<QuarterProductSales>().HasNoKey();
        }
    }

    public class QuarterProductSales
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public float ProductSales { get; set; }
        public string ShippedQuarter { get; set; }

    }
}