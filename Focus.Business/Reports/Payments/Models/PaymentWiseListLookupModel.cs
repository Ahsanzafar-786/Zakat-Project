using System;

namespace Focus.Business.Reports.Payments.Models
{
    public class PaymentWiseListLookupModel
    {
        public Guid Id { get; set; }
        public int PaymentId { get; set; }
        public int BeneficaryId { get; set; }
        public string BeneficaryName { get; set; }
        public string CashierName { get; set; }
        public string PaymentType { get; set; }
        public DateTime LastMonthPayment { get; set; }
        public DateTime NextYearPayment { get; set;}
        public DateTime PaymentDate { get;}
        public string Note { get; set; }
        public decimal Amount { get; set; }

    }
}
