using System;
using System.Collections.Generic;

namespace Focus.Business.Reports.Payments.Models
{
    public class PaymentWiseOpeningClosingModel
    {
        
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<PaymentWiseListLookupModel> PaymentList { get; set; }
        public List<PaymentWiseListLookupModel> Charitylist { get; set; }
        public decimal TransactionTotal { get;  set; }
        public decimal FundsTotal { get;  set; }
        public decimal Closing { get;  set; }
    }
}
