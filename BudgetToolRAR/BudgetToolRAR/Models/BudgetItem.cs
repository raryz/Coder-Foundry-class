using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public Boolean BudgetType { get; set; }

        public virtual BudgetCategory BudgetCategory { get; set; }

        public virtual Household Household { get; set; }
    }
}