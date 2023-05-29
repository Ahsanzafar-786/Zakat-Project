using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Focus.Domain.Entities
{
    public class Beneficiaries : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public int PaymentIntervalMonth { get; set; }
        public decimal AmountPerMonth { get; set; }
        public decimal RecurringAmount { get; set; }
        public string UgamaNo { get; set; }
        public string PhoneNo { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }
       
        public bool IsActive { get; set; }
        public bool IsRegister { get; set; }
        public Guid? AuthorizedPersonId { get; set; }
        public virtual AuthorizedPerson AuthorizedPersons { get; set; }
        public virtual ICollection<BenificaryNote> BenificaryNotes { get; set; }

    }
}
