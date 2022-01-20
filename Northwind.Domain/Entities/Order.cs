using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Northwind.Domain
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

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

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual Shipper ShipViaNavigation { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}