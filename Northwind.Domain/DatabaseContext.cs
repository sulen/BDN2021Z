using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

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


        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Error);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<CustomerCustomerDemo>()
            //    .HasOne(c => c.CustomerType)
            //    .WithMany(x => x.CustomerCustomerDemos)
            //    .HasForeignKey(x => x.CustomerTypeId);

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

            builder.Entity<Shipper>()
                .HasMany(c => c.Orders)
                .WithOne(x => x.ShipViaNavigation)
                .HasForeignKey(x => x.ShipVia);

            //builder.Entity<Customer>()
            //    .Property(x => x.Id).HasColumnName("customer_id");

            //builder.Entity<CustomerDemographic>()
            //.ToTable("customer_demographics");
        }
    }
}