using System.Collections.Generic;

namespace Focus.Business.AdminDashboard.Model
{
    public class DashboardLookupModel
    {
        public int TotalBenificary { get; set; }
        public int RegisterBenificary { get; set; }
        public int UnRegisterBenificary { get; set; }
        public int OneTimeBenificary { get; set; }
        public int MonthlyBenificary { get; set; }
        public int TotalAuthorizePerson { get; set; }
        public decimal TotalResources {get; set; }
        public int TotalUser { get; set; }
        public List<TransactionByMonthLookupModel> MonthList { get; set; }
    }
}
