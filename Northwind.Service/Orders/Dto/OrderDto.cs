using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public record OrderDto
    {
        public short OrderId { get; set; }
        public string CustomerId { get; set; }
        public short? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public short? ShipVia { get; set; }
        public float? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }

    public record ProductDto
    {
        public short ProductId { get; set; }
        public short Quantity { get; set; }
    }
}
