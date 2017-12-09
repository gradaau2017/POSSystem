using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoencoPOS.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [StringLength(150)]
        public string ProductDescription { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [StringLength(10)]
        public string UnitOfMeasure { get; set; }
        [Range(1,10000000)]
        public decimal UnitCost { get; set; }
        [Range(1, 10000000)]
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } 
    }
}
