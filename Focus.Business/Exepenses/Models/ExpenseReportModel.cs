using System;
using System.Collections.Generic;

namespace Focus.Business.Exepenses.Models
{
    public class ExpenseReportModel
    {
        public List<ExpenseLookupModel> ExpenseList { get; set; }
        public decimal ExpenseTotal { get; set; }


    }
}
