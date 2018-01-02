using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoencoPOS.Models.ViewModels
{
    public class ProductTransferLineItemViewModel
    {
        public int ProductTransferLineItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Display(Name = "Product name")]
        public string Productname { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int ProductTransferId { get; set; }
        public ProductTransfer ProductTransfer { get; set; }

    }
}