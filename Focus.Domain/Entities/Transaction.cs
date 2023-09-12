using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class Transaction : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string HijriDate { get; set; }
        public DateTime Date { get; set; }
        public string CreatedByUser { get; set; }
        public string ChequeNo { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public Guid? DocumentId { get; set; }
        public string DocumentNo { get; set; }
        public string Year { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Guid? AccountId { get; set; }
    }
}
