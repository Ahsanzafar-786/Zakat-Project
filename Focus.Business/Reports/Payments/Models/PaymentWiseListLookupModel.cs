using System;

namespace Focus.Business.Reports.Payments.Models
{
    public class PaymentWiseListLookupModel
    {
        public Guid Id { get; set; }
        public int PaymentId { get; set; }
        public int BeneficaryId { get; set; }
        public Guid Beneficary { get; set; }
        public Guid UserId { get; set; }
        public string BeneficaryName { get; set; }
        public string CashierName { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDate { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }

    }
}
