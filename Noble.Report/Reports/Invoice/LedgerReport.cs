using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.Invoice
{
    public partial class LedgerReport : DevExpress.XtraReports.UI.XtraReport
    {
        public LedgerReport(CompanyDto companyDtl,DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
        }

    }
}
