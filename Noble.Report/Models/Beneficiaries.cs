using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Windows.Media;

namespace Noble.Report.Models
{
    public class Beneficiaries 
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
        public Guid? ApprovalPersonId { get; set; }
        public Guid? PaymentTypeId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? CurrentPaymentMonth { get; set; }
        public Guid? ApprovedPaymentId { get; set; }
        public int AdvancePayment { get; set; }
        public string DurationType { get; set; }


    }
}
