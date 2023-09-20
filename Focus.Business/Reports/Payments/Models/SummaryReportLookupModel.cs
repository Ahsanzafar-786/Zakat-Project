using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.Payments.Models
{
    public class SummaryReportLookupModel
    {
        public decimal FundsReceived { get; set; }
        public decimal TotalExpense { get;  set; }
        public decimal Balance { get; set; }
        public decimal Charitydaily { get;  set; }
        public decimal Charitymonthly { get;  set; }
        public decimal TotalPayments { get;  set; }
        public decimal CashInHand { get;  set; }
    }
}
