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
            var household = db.Users.Find(User.Identity.GetUserId()).Household;
            var thisMonthData = (from category in household.Categories
                                 select new
                                 {
                                     category = category.Name,
                                     actual = (from transaction in category.Transactions
                                               where transaction.Date.Month == System.DateTime.Now.Month &&
                                                     transaction.Date.Year == System.DateTime.Now.Year &&
                                                     transaction.TransType == true
                                               select transaction.Amount).DefaultIfEmpty().Sum(),
                                     budgeted = (from budgetItem in category.BudgetItems
                                                 where budgetItem.BudgetType == true
                                                 select budgetItem.Amount).DefaultIfEmpty().Sum()

                                 }).Where(d=>d.budgeted != 0 || d.actual != 0).ToList();

            //var nonEmptyThisM = thisMonthData.Where(a => !(a.budgeted == 0 &&  a.actual == 0) ) ;        

            var lastMonthData = (from category in household.Categories
                                 select new
                                 {
                                     category = category.Name,
                                     actual = (from transaction in category.Transactions
                                               where transaction.Date.Month == System.DateTime.Now.Month - 1 &&
                                                     transaction.Date.Year == System.DateTime.Now.Year &&
                                                     transaction.TransType == true
                                               select transaction.Amount).DefaultIfEmpty().Sum(),
                                     budgeted = (from budgetItem in category.BudgetItems
                                                 where budgetItem.BudgetType == true
                                                 select budgetItem.Amount).DefaultIfEmpty().Sum()

                                 }).Where(d=>d.budgeted != 0 || d.actual != 0).ToList();

            var lastMonDonut = (from category in household.Categories
                                 select new
                                 {
                                     label = category.Name,
                                     value = (from transaction in category.Transactions
                                               where transaction.Date.Month == System.DateTime.Now.Month - 1 &&
                                                     transaction.Date.Year == System.DateTime.Now.Year &&
                                                     transaction.TransType == true
                                               select transaction.Amount).DefaultIfEmpty().Sum()
                                     
                                 }).ToList();

            var lastMonDonutI = (from category in household.Categories
                                select new
                                {
                                    label = category.Name,
                                    value = (from transaction in category.Transactions
                                             where transaction.Date.Month == System.DateTime.Now.Month - 1 &&
                                                   transaction.Date.Year == System.DateTime.Now.Year &&
                                                   transaction.TransType == false
                                             select transaction.Amount).DefaultIfEmpty().Sum()

                                }).ToList();

            return Json(new { lastMonth = lastMonthData, thisMonth = thisMonthData, lastMonthDonut = lastMonDonut, lastMonthDonutI = lastMonDonutI }, JsonRequestBehavior.AllowGet);
            //return Json(new { lastMonth = lastMonthData, thisMonth = thisMonthData }, JsonRequestBehavior.AllowGet);
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



