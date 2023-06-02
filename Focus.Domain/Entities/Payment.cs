using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Payment : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? BenificayId { get; set; }
        public virtual Beneficiaries Beneficiaries { get; set; }
        public int Code { get; set; }
        public string PaymentCode { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Date { get; set; }
        public string Year { get;set; }
        public string Period { get; set; }
        public virtual ICollection<SelectedMonth> SelectedMonth { get; set; }
    }
}
