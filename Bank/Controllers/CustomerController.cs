using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankDataAccess.Data;
using BankModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Bank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly BankCustomerContext _db;

        public CustomerController(BankCustomerContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllCustomers")]
        public ICollection<Customer> GetAllCustomers()
        {

            return _db.Customers.ToList();
        }
        [Authorize]
        [HttpGet]
        public ActionResult<Customer> GetCustomer(string email)  
        {
            string emailAddress;

            if (email == null)
                emailAddress = HttpContext.User.Identity.Name;
            else
                emailAddress = email;



            var customer = _db.Customers.Where(customer => customer.Email == emailAddress).FirstOrDefault();


            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost("AddCustomer")]
        public IEnumerable<Customer> AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            yield return customer;
        }
        [HttpPut("UpdateCustomer")]
        public ActionResult<Customer> UpdateCustomer(Customer customer)
        {
            Customer cust = _db.Customers.Where(cust => cust.CustomerId == customer.CustomerId).FirstOrDefault();
            if(customer != null)
            {
                cust.Name = customer.Name;
                cust.Email = customer.Email;
                cust.Password = customer.Password;

                _db.Customers.Update(cust);
                _db.SaveChanges();
            }
            else
            {
                return Ok("Record Not Found");
            }

            return cust;
        }
        [HttpDelete("DeleteCustomer")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            Customer customer = _db.Customers.FirstOrDefault(x => x.CustomerId == id);
            if(customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
            else
            {
                return Ok("Record Not Found");
            }

            return customer;
        }
    }
}
