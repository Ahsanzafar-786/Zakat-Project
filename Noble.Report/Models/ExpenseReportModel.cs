using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class ExpenseReportModel
    {
        public List<ExpenseLookupModel> ExpenseList { get; set; }
        public decimal ExpenseTotal { get; set; }


    }
}
