using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class ExpenseCategory : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string ExpenseCategoryName { get; set; }      
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
