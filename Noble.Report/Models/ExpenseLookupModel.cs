using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class ExpenseLookupModel
    {
        public Guid Id { get; set; }
        public Guid? ExpenseCategoryId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public Decimal Amount { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public string ExpenseCategoryName { get; set; }

    }
}
