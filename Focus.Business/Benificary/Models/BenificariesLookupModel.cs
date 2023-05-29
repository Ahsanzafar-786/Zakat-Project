using System;


namespace Focus.Business.Benificary.Models
{
    public class BenificariesLookupModel
    {
        public Guid Id { get; set; }
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string PassportNo { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public int PaymentIntervalMonth { get; set; }
        public decimal AmountPerMonth { get; set; }
        public decimal RecurringAmount { get; set; }
        public string UgamaNo { get; set; }
        public string PhoneNo { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsRegister { get; set; }
        public Guid? AuthorizedPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public string AuthorizationPersonName { get; set; }
    }
}
