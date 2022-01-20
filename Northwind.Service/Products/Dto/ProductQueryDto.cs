using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class ProductQueryDto
    {
        public string GlobalFilterTerm { get; set; }
        public string CategoryDescription { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public bool? Discontinued { get; set; }
    }
}
