using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class BudgetCategory
    {
        public int Id { get; set; }

        public int? householdId { get; set; }
        [Required]
        public string Name { get; set; }

        public BudgetCategory()
        {
            Transactions = new HashSet<Transaction>();
            BudgetItems = new HashSet<BudgetItem>();
        }

        public virtual Household Household { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
    }
}