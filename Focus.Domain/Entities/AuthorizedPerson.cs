using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class AuthorizedPerson : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public int AuthorizedPersonCode { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string PassportNo { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string IqamaNo { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Beneficiaries> Beneficiaries { get; set; }
        public virtual ICollection<BenificaryAuthorization> BenificaryAuthorization { get; set; }
    }
}
