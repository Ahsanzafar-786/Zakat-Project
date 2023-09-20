using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Focus.Domain.Entities
{
    public class Expense : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
    }
}
