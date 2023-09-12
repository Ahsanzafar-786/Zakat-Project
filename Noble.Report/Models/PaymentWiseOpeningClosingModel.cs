using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noble.Report.Models
{
    public class PaymentWiseOpeningClosingModel
    {

        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<PaymentWiseListLookupModel> PaymentList { get; set; }



    }
}
