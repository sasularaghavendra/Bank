using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BankModels.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        [MaxLength(10)]
        public long AccountNumber { get; set; }
        
        public double Balance { get; set; }
    }
}
