using System;
using System.Collections.Generic;

namespace Focus.Business.AdminDashboard.Model
{
    public class DashboardLookupModel
    {
        public int TotalBenificary { get; set; }
        public int TotalApprovalPerson { get; set; }
        public int RegisterBenificary { get; set; }
        public int UnRegisterBenificary { get; set; }
        public int OneTimeBenificary { get; set; }
        public int MonthlyBenificary { get; set; }
        public int TotalAuthorizePerson { get; set; }
        public decimal TotalResources {get; set; }
        public decimal TotalIncoming {get; set; }
        public decimal TotalOutgoing {get; set; }
        public decimal CashierTotalIncoming {get; set; }
        public decimal CashierTotalOutgoing { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal TotalFunds { get; set; }
        public decimal BeneficiaryPayment { get; set; }
        public string Date { get; set; }
        public int TotalUser { get; set; }

        public DateTime Year { get; set; }
        public List<TransactionByMonthLookupModel> MonthList { get; set; }
        public  List<BeneficiariesDurationTypeLookUpModel> BenificaryPaymentType { get; set; }
    }
}
