
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class SalesInvoice
    {
        public int SalesInvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public int SalesType { get; set; }
        public int UserId { get; set; }
        public DateTime DateSold { get; set; }
    }
}
