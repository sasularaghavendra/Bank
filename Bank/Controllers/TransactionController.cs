using BankDataAccess.Data;
using BankModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class TransactionController : ControllerBase
    {
        private readonly BankCustomerContext _db;
        public TransactionController(BankCustomerContext db)
        {
            _db = db;
        }

        [HttpGet("{id}", Name ="GetAccountDetails")]
        public ActionResult<Account> GetCustomerAccountDetails(int id)
        {
            return _db.Accounts.Include(x => x.Customer).FirstOrDefault(x => x.CustomerId == id);
        }

        [HttpGet("GetAllAccountDetails")]
        public ICollection<Account> GetAllCustomerAccountDetails()
        {
            return _db.Accounts.Include(x => x.Customer).ToList();
        }

        [HttpPut("DepositAmount")]
        public ActionResult<Account> DepositAmount(Transaction account)
        {
            Account acc = _db.Accounts.FirstOrDefault(x => x.CustomerId == account.CustomerId);

            if (acc != null)
            {
                acc.Balance = (acc.Balance + account.Balance);

                _db.Accounts.Update(acc);
                _db.SaveChanges();

                return acc;
            }
            else
            {
                return Ok("Record Not Found.");
            }
        }
        [HttpPut("WithdrawAmount")]
        public ActionResult<Account> WithdrawAmount(Transaction account)
        {
            Account acc = _db.Accounts.Include(x =>x.Customer).FirstOrDefault(x => x.CustomerId == account.CustomerId);

            if (acc != null)
            {
                if (acc.Balance < account.Balance)
                {
                    return Ok("Insufficient funds.");
                }
                else
                {
                    acc.Balance = (acc.Balance - account.Balance);
                    _db.Accounts.Update(acc);
                    _db.SaveChanges();
                    return acc;
                }
            }
            else
            {
                return Ok("Record Not found.");
            }
        }
    }
}
