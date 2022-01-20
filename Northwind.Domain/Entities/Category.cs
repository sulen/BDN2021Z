using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Northwind.Domain
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public short CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
