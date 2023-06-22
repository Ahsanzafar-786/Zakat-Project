using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Focus.Domain.Entities
{
    public class Funds : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Decimal Amount { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public Guid? CharityResouceId { get; set; }
        public virtual CharityResources CharityResources { get; set; }
        public string TypeOfTransaction { get; set; }
    }
}
