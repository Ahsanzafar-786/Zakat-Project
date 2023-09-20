
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class Expense : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public Guid? ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
