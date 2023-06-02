using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class BalanceSheetListModel
    {
        public List<BalanceSheetModel> Asset { get; set; }
        public List<BalanceSheetModel> Liability { get; set; }
        public List<BalanceSheetModel> Equity { get; set; }
        public decimal YearlyIncome { get; set; }

    }
}