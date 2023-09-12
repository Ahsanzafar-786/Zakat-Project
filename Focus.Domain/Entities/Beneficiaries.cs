using Focus.Domain.Enum;
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
        public string NameAr { get; set; }
        public string PassportNo { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Reason { get; set; }
        public int PaymentIntervalMonth { get; set; }
        public decimal AmountPerMonth { get; set; }
        public decimal LastPaymentAmount { get; set; }
        public decimal RecurringAmount { get; set; }
        public string UgamaNo { get; set; }
        public string PhoneNo { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        public bool IsActive { get; set; }
        public bool IsRegister { get; set; }
        public Guid? AuthorizedPersonId { get; set; }
        public virtual AuthorizedPerson AuthorizedPersons { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public virtual ApprovalPerson ApprovalPersons { get; set; }
        public virtual ICollection<BenificaryNote> BenificaryNotes { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public virtual PaymentType PaymentTypes { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? CurrentPaymentMonth { get; set; }
        public Guid? ApprovedPaymentId { get; set; }
        public int AdvancePayment { get; set; }
        public string DurationType { get; set; }
        public virtual ICollection<BenificaryAuthorization> BenificaryAuthorization { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<CharityTransaction> CharityTransactions { get; set; }


    }
}
