using System;

namespace Focus.Business.ExpenseCategories.Model
{
    public class ExpenseCategoryLookupModel
    {
        public Guid Id { get; set; }
        public string ExpenseCategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }   
        public bool IsActive { get; set; }
    }
}
