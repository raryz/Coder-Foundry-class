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

        //public JsonResult GetDataAjax()
        //{
        //    var data = new[] {
        //        new{ y= "2006", a= 100, b= 90 },
        //        new{ y= "2007", a= 75,  b= 65 }
        //};
        //    return Json(data);
        //}

        //GET: DashBrd
        public ActionResult MainAdmin()
        {
            var model = new DashBrdViewModel();

            var user = db.Users.Find(User.Identity.GetUserId());
            var hhid = user.HouseholdId;
            var accounts = db.Accounts.Where(a => a.HouseholdId == hhid);
            var transactions = db.Transactions.Where(a => a.Account.HouseholdId == hhid);

            foreach (var a in accounts)
            {
                model.accountInfo.Add(new AccountInfo { AccountName = a.Name, AccountBalance = a.Balance, AcctId = a.Id });
            }

            foreach (var trx in transactions)
            {
                model.transactionInfo.Add(new TransactionInfo { AcctName = trx.Account.Name, Amount = trx.Amount, Date = trx.Date, Description = trx.Description });
            }

            model.transactionInfo = model.transactionInfo.OrderByDescending(d => d.Date).Take(4).ToList();
            return View(model);
        }
    }

}



