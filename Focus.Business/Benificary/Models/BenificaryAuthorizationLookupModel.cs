using System;

namespace Focus.Business.Benificary.Models
{
    public class BenificaryAuthorizationLookupModel
    {
        public Guid Id { get; set; }
        public Guid? BenficaryId { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
