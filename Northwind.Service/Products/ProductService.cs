using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class ProductService
    {
        private readonly DatabaseContext _dbContext;

        public ProductService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(ProductQueryDto productQueryDto)
        {
            IQueryable<Product> ordersQuery = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier);

            if (productQueryDto.Discontinued.HasValue)
            {
                ordersQuery = ordersQuery.Where(x => x.Discontinued == Convert.ToInt32(productQueryDto.Discontinued.Value));
            }
            if (productQueryDto.CategoryDescription is not null)
            {
                ordersQuery = ordersQuery.Where(x => x.Category.Description.Contains(productQueryDto.CategoryDescription));
            }
            if (productQueryDto.ProductName is not null)
            {
                ordersQuery = ordersQuery.Where(x => x.ProductName.Contains(productQueryDto.ProductName));
            }
            if (productQueryDto.CompanyName is not null)
            {
                ordersQuery = ordersQuery.Where(x => x.Supplier.CompanyName.Contains(productQueryDto.CompanyName));
            }
            if (productQueryDto.GlobalFilterTerm is not null)
            {
                ordersQuery = ordersQuery.Where(x => x.Supplier.CompanyName.Contains(productQueryDto.GlobalFilterTerm)
                    || x.ProductName.Contains(productQueryDto.GlobalFilterTerm)
                    || x.Category.Description.Contains(productQueryDto.GlobalFilterTerm)
                    || x.Supplier.ContactName.Contains(productQueryDto.GlobalFilterTerm)
                    || x.Supplier.ContactTitle.Contains(productQueryDto.GlobalFilterTerm)
                    || x.ProductName.Contains(productQueryDto.GlobalFilterTerm)
                );
            }
            return await ordersQuery.ToListAsync();
        }

        public async Task<IEnumerable<QuarterProductSales>> GetQuarterProductSales()
        {
            var result = await _dbContext.QuarterProductSales
                .FromSqlRaw("SELECT * FROM  product_sales_for_1997_by_quarter()")
                .ToListAsync();

            return result;
        }
    }
}