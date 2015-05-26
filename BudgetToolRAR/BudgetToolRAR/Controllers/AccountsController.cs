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
        public ActionResult AccountsIndexLb()
        {
            // need to find householdId for user and only display accounts for that household
            return View(db.Accounts.ToList());
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
            return View(transactions.ToList());

            //Account account = db.Accounts.Find(id);
            //if (account == null)
            //{
            //    return HttpNotFound();
            //}                          could have done it this way and returned the account
            //return View(account);      this would have been similar to Comments under Posts
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
            ViewBag.AccountIdId = new SelectList(db.Accounts, "Id", "Name");
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
                if (transactiontype == "Expense")
                {
                    transaction.TransType = true; // Expense = true
                }
                if (transactiontype == "Income")
                {
                    transaction.TransType = false; // Income = false
                }
               
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("AccountsIndexLb");
            }

            return View(transaction);
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
