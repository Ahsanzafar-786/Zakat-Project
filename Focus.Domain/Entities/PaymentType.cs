using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class PaymentType : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Beneficiaries> Beneficiaries { get; set; }
    }
}
