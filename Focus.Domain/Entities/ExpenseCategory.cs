using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Focus.Domain.Entities
{
    public class ExpenseCategory : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string ExpenseCategoryName { get; set; }      
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
