using BudgetToolRAR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;

namespace BudgetToolRAR.Controllers
{
    public class DashBrdController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: DashBrd
        public ActionResult MainAdmin()
        {
            var model = new DashBrdViewModel();

            var user = db.Users.Find(User.Identity.GetUserId()); 
            var hhid = user.HouseholdId;
            var accounts = db.Accounts.Where(a => a.HouseholdId == hhid);
   
            foreach (var a in accounts)
            {
                model.accountInfo.Add(new AccountInfo { AccountName = a.Name, AccountBalance = a.Balance });
            

                var transactions = db.Transactions.Where((t => t.AccountId == a.Id));    //   && (t => t.AccountId == id)) ;


                foreach (var t in transactions)
                {
                    model.transactionInfo.Add(new TransactionInfo { AcctName = t.Account.Name, Amount = t.Amount, Date = t.Date, Description = t.Description});
                }                
            }
model.transactionInfo = model.transactionInfo.OrderByDescending(d => d.Date).Take(4).ToList();
            return View(model);
        }
    }
}