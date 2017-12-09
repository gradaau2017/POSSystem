using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
