using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class SalesLineItem
    {
        public int SalesLineItemId { get; set; }
        [Required]
        public int SalesInvoiceId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public Decimal Quantity { get; set; }
        [Required]
        public Decimal UnitPrice { get; set; }
        public virtual SalesInvoice SalesInvoice { get; set; }
    }
}
