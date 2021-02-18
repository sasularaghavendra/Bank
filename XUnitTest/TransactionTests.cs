using Bank.Controllers;
using BankDataAccess.Data;
using BankModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class TransactionTests
    {
        private readonly TransactionController transactionController;
        public static DbContextOptions<BankCustomerContext> dbContextOptions { get; }
        public static string connectionString = "Server=DESKTOP-QRCLU6I;Database=BankCustomer;Trusted_Connection=True;";
         
        static TransactionTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<BankCustomerContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public TransactionTests()
        {
            var context = new BankCustomerContext(dbContextOptions);
            transactionController = new TransactionController(context);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GetCustomerAccountDetailsTest(int customerId)
        {
            //Arrange
            var customer = new Transaction { CustomerId = customerId };
            //Act
            var data = transactionController.GetCustomerAccountDetails(customer.CustomerId);
            //Assert
            Assert.NotNull(data.Value);
        }
        [Fact]
        public void GetAllCustomerAccountDetailsTest()
        {
            //Act
            var data = transactionController.GetAllCustomerAccountDetails();
            //Assert
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData(1, 5000, 47000)]
        [InlineData(1, 1000, 48000)]
        [InlineData(1, 6000, 54000)]
        public void Customer_AmountDeposit_ReturnsBalance(int customerId, int balance, int totalBalance)
        {
            Transaction transaction = new Transaction { CustomerId = customerId, Balance = balance };
 
            var result = transactionController.DepositAmount(transaction);

            Assert.Equal(totalBalance, result.Value.Balance);
        }
        [Theory]
        [InlineData(4, 1000, 11000)]
        [InlineData(4, 1000, 10000)]
        [InlineData(4, 6000, 4000)]
        public void Customer_AmountWithdraw_ReturnsDetailsNotNull(int customerId, int balance, int totalBalance)
        {
            Transaction transaction = new Transaction { CustomerId = customerId, Balance = balance };

            var result = transactionController.WithdrawAmount(transaction);

            Assert.Equal(totalBalance, result.Value.Balance);
            
        }

    }
}


