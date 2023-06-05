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
        public decimal Amount { get; set; }
        public DateTime? DoucmentDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? CharityTransactionDate { get; set; }
        public string Year { get; set; }
    }
}
