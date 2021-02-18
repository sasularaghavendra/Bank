using System;
using System.Collections.Generic;
using System.Text;

namespace BankModels.Models
{
    public class Transaction
    {
        public int CustomerId { get; set; }
        public long Balance { get; set; }
    }
}
