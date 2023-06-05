using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.Invoice
{
    public partial class PaymentReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PaymentReport(CompanyDto companyDetail,PaymentLookupModel Payments)
        {
            InitializeComponent();

            PaymentDtl.DataSource = Payments;
            CompanyDtl.DataSource = companyDetail;
        }

    }
}
