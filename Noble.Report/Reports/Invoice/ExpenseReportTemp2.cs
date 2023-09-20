using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
namespace Noble.Report.Reports.Invoice
{
    public partial class ExpenseReportTemp2 : DevExpress.XtraReports.UI.XtraReport
    {
        public ExpenseReportTemp2(SummaryReportLookupModel summery)
        {
            InitializeComponent();
            SummeryInfo.DataSource = summery;
        }

    }
}
