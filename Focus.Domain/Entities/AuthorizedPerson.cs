using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class AuthorizedPerson : BaseEntity
    {
        public int AuthorizedPersonCode { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Beneficiaries> Beneficiaries { get; set; }

    }
}
