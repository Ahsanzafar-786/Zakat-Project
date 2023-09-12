using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class ApprovalPerson : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int AprovalPersonId { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Beneficiaries> Beneficiaries { get; set; }
        public virtual ICollection<BenificaryAuthorization> BenificaryAuthorization { get; set; }

    }
}
