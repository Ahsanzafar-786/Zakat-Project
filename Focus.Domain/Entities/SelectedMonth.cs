using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class SelectedMonth : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime? SelectMonth { get; set; }
        public Guid? PaymentId { get; set; }
        public virtual Payment Payments { get; set; }

        public decimal Amount { get; set; }
    }
}
