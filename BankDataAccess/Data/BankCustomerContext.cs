using BankModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankDataAccess.Data
{
    public class BankCustomerContext : DbContext
    {
        public BankCustomerContext(DbContextOptions<BankCustomerContext> options) :base(options)
        {
             
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
    }
}
