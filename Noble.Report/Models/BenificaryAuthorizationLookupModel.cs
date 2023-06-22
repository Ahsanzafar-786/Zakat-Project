using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class BenificaryAuthorizationLookupModel
    {
        public Guid? Id { get; set; }
        public Guid? BenficaryId { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public string ApprovalPersonName { get; set; }
        public string AuthorizationPersonName { get; set; }
        public string AuthorizationPersonNameAr { get; set; }
        public int AuthorizationPersonCode { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}