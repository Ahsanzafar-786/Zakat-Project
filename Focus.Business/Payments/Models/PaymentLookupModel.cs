using System;
using System.Collections.Generic;

namespace Focus.Business.Payments.Models
{
    public class PaymentLookupModel
    {
        public Guid? Id { get; set; }
        public Guid? BenificayId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentCode { get; set; }
        public string UserId { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Date { get; set; }
        public int? PaymentMonth { get; set; }
        public string Year { get; set; }
        public string Note { get; set; }
        public int Code { get; set; }
        public string Period { get; set; }
        public string BenificaryName { get; set; }
        public string BenificaryNameAr { get; set; }
        public bool IsVoid { get; set; }
        public string HijriYear { get; set; }
        public string HijriMonth { get; set; }
        public string Cashier { get; set; }
        public bool AllowVoid { get; set; }
        public List<SelectedMonthLookupModel> SelectedMonth { get; set; }
    }
}
