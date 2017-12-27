using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class ProductReceiveInvoice
    {
        public int ProductReceiveInvoiceId { get; set; }
        public DateTime DateReceived { get; set; }
        public int BranchId { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<ProductReceiveLineItem> ProductReceiveLineItems { get; set; }
    }
}
