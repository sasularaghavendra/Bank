using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BankModels.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } 
        [Required]
        [MaxLength(20)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string  Password { get; set; }
    }
}
