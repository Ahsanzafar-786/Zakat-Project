using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class CharityTransaction : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? DoucmentId { get; set; }
        public string DoucmentCode { get; set; }
        public string DocumentName { get; set; }
        public Guid? BenificayId { get; set; }
        public virtual Beneficiaries Beneficiaries { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DoucmentDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? CharityTransactionDate { get; set; }
        public bool IsVoid { get; set; }
        public string HijriYear { get; set; }
        public string HijriMonth { get; set; }
        public string Year { get; set; }
        
        public string? UserId { get; set; }
        

    }
}
