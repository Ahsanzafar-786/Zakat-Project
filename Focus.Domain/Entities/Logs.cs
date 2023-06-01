using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class Logs : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? BenificaryId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string PhoneNo { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public decimal RecurringAmount { get; set; }
        public int AdvancePayment { get; set; }
        public decimal AmountPerMonth { get; set; }
        public DateTime? StartMonth { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public Guid? ApprovedPaymentId { get; set; }
        public string DurationType { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
