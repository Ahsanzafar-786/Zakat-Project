using Focus.Business.Users;
using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Focus.Domain.Entities
{
    public class Payment : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? BenificayId { get; set; }
        public virtual Beneficiaries Beneficiaries { get; set; }

        public int Code { get; set; }
        public string PaymentCode { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime? Month { get; set; }
        public DateTime? Date { get; set; }
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public string HijriYear { get; set; }
        public string HijriMonth { get; set; }
        public string Period { get; set; }
        public bool IsVoid { get; set; }
        public bool AllowVoid { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<SelectedMonth> SelectedMonth { get; set; }


    }
}
