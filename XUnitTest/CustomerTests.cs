using Bank.Controllers;
using BankDataAccess.Data;
using BankModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace XUnitTest
{
    public class CustomerTests
    {
       
        private readonly CustomerController customerController;
        public static DbContextOptions<BankCustomerContext> dbContextOptions { get; }
        public static string connectionString = "Server=DESKTOP-QRCLU6I;Database=BankCustomer;Trusted_Connection=True;";

        static CustomerTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<BankCustomerContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public CustomerTests()
        {
            var context = new BankCustomerContext(dbContextOptions);
            customerController = new CustomerController(context);
        }

        [Theory]
        [InlineData("RaGav@gmail.com")]
        [InlineData("sidhu@gmail.com")]
        [InlineData("mari@yahoo.com")]
        public void SuccessLoginTest(string email) 
        {
            //Arrange
            var customer = new Customer { Email = email };
            //Act
            var data = customerController.GetCustomer(customer.Email);
            //Assert
            Assert.IsType<ActionResult<Customer>>(data);          
        }
        [Theory]
        [InlineData("Test@gmail.com")]
        [InlineData("Fail@yahoo.com")]
        public void FailLoginTest(string email)
        {
            //Arrange
            var customer = new Customer { Email = email };
            //Act
            var data = customerController.GetCustomer(customer.Email);
            //Assert
            Assert.Contains("Not", data.Result.ToString());
        }
        [Fact]
        public void Login_GetCustomerTest_NotNull()
        {
            //Arrange
            var customer = new Customer { Email = "RaGav@gmail.com"};
            //Act
            var data = customerController.GetCustomer(customer.Email);
            //Assert
            Assert.NotNull(data.Value);
        }
        [Fact]
        public void Login_GetCustomerTest_Null()
        {
            //Arrange
            var customer = new Customer { Email = "Test" };
            //Act
            var data = customerController.GetCustomer(customer.Email);
            //Assert
            Assert.Null(data.Value);
        }
        [Theory]
        [InlineData("RaGav@gmail.com")]
        [InlineData("sidhu@gmail.com")]
        [InlineData("mari@yahoo.com")]
        public void Login_GetCustomerTest_Equal(string email)
        {
            //Arrange
            var customer = new Customer { Email = email };
            //Act
            var data = customerController.GetCustomer(customer.Email);
            //Assert
            Assert.Equal(email, data.Value.Email.ToString());
        }

    }
}
