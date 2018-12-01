using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Utility.ViewModel
{
   public class TransactionInputModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(15)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Card must be numeric")]
        public string  accountNumber { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public string currency { get; set; }
    }
}
