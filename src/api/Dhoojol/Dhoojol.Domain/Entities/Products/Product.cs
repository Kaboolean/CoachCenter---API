using Dhoojol.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Domain.Entities.Products
{
    public class Product : Entity
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string Make { get; set; } = null!;
        public double Price { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int Stock { get; set; }
    }
}
