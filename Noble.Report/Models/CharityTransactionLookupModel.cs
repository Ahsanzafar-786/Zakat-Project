using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class CharityTransactionLookupModel
    {
        public Guid? Id { get; set; }

        public Guid? DoucmentId { get; set; }
        public Guid? BenificayId { get; set; }
        public string DoucmentCode { get; set; }
        public string benificaryName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DoucmentDate { get; set; }
        public DateTime? Month { get; set; }
        public int? PaymentMonths { get; set; }
        public DateTime? CharityTransactionDate { get; set; }
        public string Year { get; set; }
        public string Years { get; set; }
        public bool IsVoid { get; set; }
        public string HijriYear { get; set; }
        public string HijriMonth { get; set; }


    }
}