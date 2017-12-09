using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoencoPOS.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        [Required]
        [StringLength(50)]
        public string BranchName { get; set; }
        [StringLength(50)]
        public string BranchLocation { get; set; }
        [StringLength(150)]
        public string BranchDescription { get; set; }
    }
}
