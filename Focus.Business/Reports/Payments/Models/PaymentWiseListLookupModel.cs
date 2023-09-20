using Focus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Focus.Business.Reports.Payments.Models
{
    public class PaymentWiseListLookupModel
    {
        public Guid? Id { get; set; }
        public string PaymentId { get; set; }
        public string BeneficaryId { get; set; }
        public Guid? Beneficary { get; set; }
        public Guid? UserId { get; set; }
        public string BeneficaryName { get; set; }
        public string CashierName { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDate { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public int PaymentIntervalMonth { get;  set; }
       
        public string IsRegistered { get;  set; }
        public string PaymentMonth { get;  set; }
        public List<DateTime?> SelectedMonth { get; set; }
        public string Description { get;  set; }
        public string Transactiontype { get;  set; }
    }
       
}
