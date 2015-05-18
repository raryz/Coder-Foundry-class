using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class Invite
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int HouseholdId { get; set; }

        public virtual Household Household { get; set; }
    }
}