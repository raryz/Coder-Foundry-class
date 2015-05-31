using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetToolRAR.Models;
using Microsoft.AspNet.Identity;

namespace BudgetToolRAR.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts
        public ActionResult BudgetIndexLb()
        {

            return View();
        }

        // GET: Accounts
        public ActionResult AccountsIndexLb()
        {
            // need to find householdId for user and only display accounts for that household

            var user = db.Users.Find(User.Identity.GetUserId());
            var hhid = user.HouseholdId;
            var accounts = db.Accounts.Where(a => a.HouseholdId == hhid);

            return View(accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/TransactionDetailsLb/5
        public ActionResult TransactionDetailsLb(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var transactions = db.Transactions.Where(t => t.AccountId == id);
            var acctInfo = db.Accounts.FirstOrDefault(act => act.Id == id);
            ViewBag.prevBalance = acctInfo.Balance;

            return View(transactions.ToList());

            //Account account = db.Accounts.Find(id);
            //if (account == null)
            //{
            //    return HttpNotFound();
            //}                          could have done it this way and returned the account
            //return View(account);      this would have been similar to Comments under Posts
        }

        // GET: Accounts/TransactionDetailsLb/5
        public ActionResult BudgetDetailsLb()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var hhid = user.HouseholdId;
            var budgetitems = db.BudgetItems.Where(t => t.HouseholdId == hhid);
            return View(budgetitems.ToList());

        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }
        // GET: Accounts/CreateCategoryLb
        public ActionResult CreateCategoryLb()
        {
            return View();
        }

        // POST: Accounts/CreateCategoryLb
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategoryLb([Bind(Include = "Id,Name")] BudgetCategory budgetcategory)
        {
            if (ModelState.IsValid)
            {

                db.BudgetCategories.Add(budgetcategory);

                db.SaveChanges();
                return RedirectToAction("AccountsIndexLb");
            }

            return RedirectToAction("AccountsIndexLb");
        }


        // GET: Accounts/AccountsCreateLb
        public ActionResult AccountsCreateLb()
        {
            return View();
        }

        // POST: Accounts/AccountsCreateLb
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountsCreateLb([Bind(Include = "Id,Name,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                account.HouseholdId = user.HouseholdId.Value;
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("AccountsIndexLb");
            }

            return View(account);
        }

        // GET: Accounts/TransactionCreateLb
        public ActionResult TransactionCreateLb()
        {
            ViewBag.BudgetCategoryId = new SelectList(db.BudgetCategories, "Id", "Name");

            var user = db.Users.Find(User.Identity.GetUserId());
            var hhid = user.HouseholdId;
            var accounts = db.Accounts.Where(a => a.HouseholdId == hhid);

            ViewBag.AccountId = new SelectList(accounts, "Id", "Name");
            return View();
        }

        // POST: Accounts/TransactionCreateLb
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransactionCreateLb([Bind(Include = "Id,Date,Description,Amount,ReconciledAmount,BudgetCategoryId,AccountId")] Transaction transaction, string transactiontype)
        {
            if (ModelState.IsValid)
            {
                var account = db.Accounts.FirstOrDefault(acct => acct.Id == transaction.AccountId);

                if (transactiontype == "Expense")
                {
                    transaction.TransType = true; // Expense = true
                    account.Balance = account.Balance - transaction.Amount;
                }
                if (transactiontype == "Income")
                {
                    transaction.TransType = false; // Income = false
                    account.Balance = account.Balance + transaction.Amount;
                }

                db.Entry(account).State = EntityState.Modified;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("AccountsIndexLb");

                //var income = transactions.Where(y => y.TransType == false).Select(t=>t.Amount).DefaultIfEmpty().Sum();
                //var expense = transactions.Where(y => y.TransType == true).Select(t=>t.Amount).DefaultIfEmpty().Sum();
                //var balance = income - expense;
                //var curBalance = prevBalance - balance;
            }

            ViewBag.BudgetCategoryId = new SelectList(db.BudgetCategories, "Id", "Name");

            var user = db.Users.Find(User.Identity.GetUserId());
            var hhid = user.HouseholdId;
            var accounts = db.Accounts.Where(a => a.HouseholdId == hhid);

            ViewBag.AccountId = new SelectList(accounts, "Id", "Name");
            return View(transaction);
        }

        // GET: Accounts/BudgetCreateLb
        public ActionResult BudgetCreateLb()
        {
            ViewBag.BudgetCategoryId = new SelectList(db.BudgetCategories, "Id", "Name");

            return View();
        }

        // POST: Accounts/BudgetCreateLb
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BudgetCreateLb([Bind(Include = "Id,Name,Amount,BudgetCategoryId,HouseholdId")] BudgetItem budgetitem, string budtype)
        {
            if (ModelState.IsValid)
            {
                if (budtype == "Expense")
                {
                    budgetitem.BudgetType = true; // Expense = true
                }
                if (budtype == "Income")
                {
                    budgetitem.BudgetType = false; // Income = false
                }

                var user = db.Users.Find(User.Identity.GetUserId());
                budgetitem.HouseholdId = user.HouseholdId.Value;
                db.BudgetItems.Add(budgetitem);
                db.SaveChanges();
                return RedirectToAction("BudgetDetailsLb");
            }

            return View(budgetitem);
        }
        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/AccountsEditLb/5
        public ActionResult AccountsEditLb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/AccountsEditLb/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountsEditLb([Bind(Include = "Id,Name,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                account.HouseholdId = user.HouseholdId.Value;

                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AccountsIndexLb");
            }
            return View(account);
        }

        // GET: Accounts/TransactionEditLb/5
        public ActionResult TransactionEditLb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);

            var user = db.Users.Find(User.Identity.GetUserId());
            var hhId = user.HouseholdId.Value;
            var accounts = db.Accounts.Where(a => a.HouseholdId == hhId);

            ViewBag.BudgetCategoryId = new SelectList(db.BudgetCategories, "Id", "Name", transaction.BudgetCategoryId);
            ViewBag.AccountId = new SelectList(accounts, "Id", "Name", transaction.AccountId);


            //ViewBag.BudgetCategoryId = new SelectList(db.BudgetCategories, "Id", "Name");

            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Accounts/TransactionEditLb/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransactionEditLb([Bind(Include = "Id,Date,Description,Amount,ReconciledAmount,BudgetCategoryId,AccountId")] Transaction transaction, string trantype)
        {
            if (ModelState.IsValid)
            {
                var oldtransaction = db.Transactions.AsNoTracking().Where(tr => tr.Id == transaction.Id).FirstOrDefault();

                var account = db.Accounts.FirstOrDefault(acct => acct.Id == transaction.AccountId);

                if (trantype == "Expense")
                {
                    transaction.TransType = true;
                    if (transaction.Amount != oldtransaction.Amount)
                    {
                        account.Balance = account.Balance - (transaction.Amount - oldtransaction.Amount);
                    }
                }
                else
                {
                    transaction.TransType = false;
                    if (transaction.Amount != oldtransaction.Amount)
                    {
                        account.Balance = account.Balance + (transaction.Amount - oldtransaction.Amount);
                    }
                }

                db.Entry(account).State = EntityState.Modified;
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AccountsIndexLb");
            }
            return View(transaction);
        }

        // GET: Accounts/BudgetEditLb/5
        public ActionResult BudgetEditLb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetitem = db.BudgetItems.Find(id);

            //var budId = budgetitem.BudgetCategoryId;
            // the budgetitem.BudgetCategoryId object is being passed in the SelectList as the default selected value

            ViewBag.BudgetCategoryId = new SelectList(db.BudgetCategories, "Id", "Name", budgetitem.BudgetCategoryId);

            if (budgetitem == null)
            {
                return HttpNotFound();
            }
            return View(budgetitem);
        }

        // POST: Accounts/BudgetEditLb/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BudgetEditLb([Bind(Include = "Id,Name,Amount,BudgetCategoryId,BudgetType,HouseholdId")] BudgetItem budgetitem, string budtype)
        {
            if (ModelState.IsValid)
            {
                if (budtype == "Expense")
                {
                    budgetitem.BudgetType = true;
                }
                else
                {
                    budgetitem.BudgetType = false;
                }
                db.Entry(budgetitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BudgetDetailsLb");
            }
            return View(budgetitem);
        }


        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Accounts/AccountsDeleteLb/5
        public ActionResult AccountsDeleteLb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/AccountsDeleteLb/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountsDeleteLb(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("AccountsIndexLb");
        }

        // GET: Accounts/TransactionDeleteLb/5
        public ActionResult TransactionDeleteLb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // POST: Accounts/TransactionDeleteLb/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransactionDeleteLb(int id)
        {
            Transaction transaction = db.Transactions.Find(id);

            var account = db.Accounts.FirstOrDefault(acct => acct.Id == transaction.AccountId);

            if (transaction.TransType == true)  // Expense = true
            {
                account.Balance = account.Balance + transaction.Amount;   // opposite of create, removing an expense
            }
            if (transaction.TransType == false)   // Income = false
            {
                account.Balance = account.Balance - transaction.Amount;   // opposite of create, removing income
            }

            db.Entry(account).State = EntityState.Modified;

            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("AccountsIndexLb");
        }

        // GET: Accounts/BudgetDeleteLb/5
        public ActionResult BudgetDeleteLb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetitem = db.BudgetItems.Find(id);
            if (budgetitem == null)
            {
                return HttpNotFound();
            }

            return View(budgetitem);
        }

        // POST: Accounts/BudgetDeleteLb/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BudgetDeleteLb(int id)
        {
            BudgetItem budgetitem = db.BudgetItems.Find(id);
            db.BudgetItems.Remove(budgetitem);
            db.SaveChanges();
            return RedirectToAction("BudgetDetailsLb");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
