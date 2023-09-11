using System;
using System.Collections.Generic;

namespace Focus.Business.Reports.Payments.Models
{
    public class PaymentWiseOpeningClosingModel
    {
        
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<PaymentWiseListLookupModel> PaymentList { get; set; }



    }
}
