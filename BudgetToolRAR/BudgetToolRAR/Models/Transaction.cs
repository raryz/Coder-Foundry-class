using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public Boolean TransType { get; set; }

        public DateTimeOffset Date { get; set; }

        public Decimal Amount { get; set; }

        public Decimal ReconciledAmount { get; set; }

        public int AccountId { get; set; }
        public int BudgetCategoryId { get; set; }

        public virtual Account Account { get; set; }


        public virtual BudgetCategory BudgetCategory { get; set; }

    }
}