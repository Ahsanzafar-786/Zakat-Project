using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class PaymentWiseListLookupModel
    {
        public int PaymentId { get; set; }
        public int BeneficaryId { get; set; }
        public string BeneficaryName { get; set; }
        public string CashierName { get; set; }
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; }
        public string Note { get; set; }
        public decimal Amount { get; set; }

    }
}