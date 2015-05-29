using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetToolRAR.Models
{
    public class DashBrdViewModel
    {
        public DashBrdViewModel()
        {
            accountInfo = new List<AccountInfo>();
            transactionInfo = new List<TransactionInfo>();
        }

        public List<AccountInfo> accountInfo { get; set; }
        public List<TransactionInfo> transactionInfo { get; set; }
    }

    public class AccountInfo
    {
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public int AcctId { get; set; }
    }

    public class TransactionInfo
    {
        public DateTimeOffset Date { get; set; }
        public string AcctName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }

}