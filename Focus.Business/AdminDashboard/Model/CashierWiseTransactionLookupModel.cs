using System.Collections.Generic;

namespace Focus.Business.AdminDashboard.Model
{
    public class CashierWiseTransactionLookupModel
    {
       public List<CashierWiseIncomingBalanceLookupModel> CashierWiseIncomingBalance { get; set; }
       public List<CashierWiseOutgoingBalanceLookupModel> CashierWiseOutgoinBalance { get; set; }

    }
}
