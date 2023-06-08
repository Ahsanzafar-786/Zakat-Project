using Focus.Business.Payments.Models;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class PaymentLookupModel
    {
        public Guid? Id { get; set; }
        public Guid? BenificayId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentCode { get; set; }
        public string UserId { get; set; }
        public DateTime? Month { get; set; }
        public int? PaymentMonth { get; set; }
        public string Year { get; set; }
        public int Code { get; set; }
        public string Period { get; set; }
        public string BenificaryName { get; set; }
        public List<SelectedMonthLookupModel> SelectedMonth { get; set; }
    }
}
