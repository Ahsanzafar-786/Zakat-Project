using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.Payments.Models;
using Focus.Business.Transactions.Models;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Business.Benificary.Models
{
    public class BenificariesLookupModel
    {
        public Guid Id { get; set; }
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string NameAr { get; set; }
        public string PassportNo { get; set; }
        public string Address { get; set; }
        public string Year { get; set; }
        public string EndYear { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public int PaymentIntervalMonth { get; set; }
        public decimal AmountPerMonth { get; set; }
        public decimal RecurringAmount { get; set; }
        public decimal OpeningBalance { get; set; }
        public string UgamaNo { get; set; }
        public string PhoneNo { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsRegister { get; set; }
        public bool IsCustomize { get; set; }
        public Guid? AuthorizedPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public string AuthorizationPersonName { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeNameAr { get; set; }
        public string ApprovalPersonName { get; set; }

        public List<BenificaryAuthorizationLookupModel> BenificaryAuthorization { get; set; }
        public List<PaymentLookupModel> PaymentLists { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CurrentPaymentMonth { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime? StartMonth { get; set; }
        public Guid? ApprovedPaymentId { get; set; }
        public int AdvancePayment { get; set; }
        public int? PaymentType { get; set; }
        public string DurationType { get; set; }
        public int? FirstMonth { get; set; }
        public int? EndMonth { get; set; }
        public string Reason { get; set; }
        public string StartMonthAndYear { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        public List<CharityTransactionLookupModel> CharityTransactions { get; set; }

        public bool IsDisable { get; set; }
        public bool IsDailyPayment { get; set; }
    }
}
