using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class BenificaryAuthorization : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? BenficaryId { get; set; }
        public virtual Beneficiaries Beneficiaries { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public virtual AuthorizedPerson AuthorizedPerson { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public virtual ApprovalPerson ApprovalPerson { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
