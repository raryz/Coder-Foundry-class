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

        public JsonResult GetDataAjax()
        {
            var data = new[] {
                new{ x= 1, y= 10 },
                new{ x= 2,  y= 15 },
                new{ x= 3,  y= 20}
        };
            return Json(data);
        }

        public JsonResult GetDataAjax2()
        {
            var data2 = new[] {
                new{ y= "2006", a= 100, b= 90 },
                new{ y= "2007", a= 75,  b= 65 },
                new{ y= "2007", a= 50,  b= 40}
        };
            return Json(data2);
        }

        public ActionResult GetChartData()
        {
            return View();
        }
    }
    //var data = new[] {
    //            new{ y= "2006", a= 100, b= 90 },
                //new{ y= "2007", a= 75,  b= 65 },
                //new{ y= "2007", a= 50,  b= 40}

}



