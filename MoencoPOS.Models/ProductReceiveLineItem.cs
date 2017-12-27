using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class ProductReceiveLineItem
    {
        public int ProductReceiveLineItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 10000000)]
        [DisplayName("Unit Cost")]
        public decimal UnitCost { get; set; }

        public int ProductReceiveInvoiceId { get; set; }
        public ProductReceiveInvoice ProductReceiveInvoice { get; set; }
    }
}
