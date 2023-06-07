using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.AdminDashboard.Model
{
    public class TransactionByMonthLookupModel
    {
        public int Month {get; set;}
        public string MonthName { get; set;}
        public decimal Amount { get; set;}

    }
}
