using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class Household
    {
        public Household()
        {
            this.Accounts = new HashSet<Account>();
            this.BudgetItems = new HashSet<BudgetItem>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public  virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
    }
}