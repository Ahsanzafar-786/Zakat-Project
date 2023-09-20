using System;
using System.Collections.Generic;

namespace Focus.Business.Payments.Models
{
    public class PaymentLookupModel
    {
        public Guid? Id { get; set; }
        public Guid? BenificayId { get; set; }
        public decimal Amount { get; set; }
        public string AuthorizePersonName { get; set; }
        public string ApprovalPersonName { get; set; }
        public decimal LastPaymentAmount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public string NextPaymentMonth { get; set; }
        public string NextPaymentYear { get; set; }
        public string PaymentType { get; set; }
        public string PaymentCode { get; set; }
        public int BenificaryCode { get; set; }
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
        public string AuthorizePerson { get; set; }
        public string Nationality { get; set; }
        public string UgamaNo { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public bool AllowVoid { get; set; }
        public bool IsRegister { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public Guid? AuthorizePersonId { get; set; }
        public List<SelectedMonthLookupModel> SelectedMonth { get; set; }
        public string PaymentTypeAr { get;  set; }
        public decimal TotalAmount { get;  set; }
        public DateTime? EndMonth { get;  set; }
        public DateTime? NextMonth { get;  set; }
        public string DurationType { get;  set; }
    }
}
