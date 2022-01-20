using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace Northwind.Domain
{
    public partial class CustomerCustomerDemo
    {
        [Key]
        public string CustomerId { get; set; }

        public string CustomerTypeId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }     
        public virtual CustomerDemographic CustomerType { get; set; }
    }
}
