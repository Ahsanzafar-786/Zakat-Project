using System;

namespace Focus.Business.ApprovalsPerson.Model
{
    public class ApprovalPersonLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int AprovalPersonId { get; set; }
        public bool IsActive { get; set; }
    }
}
