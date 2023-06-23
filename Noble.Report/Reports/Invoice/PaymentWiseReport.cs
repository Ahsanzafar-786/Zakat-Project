using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;

namespace Noble.Report.Reports.Invoice
{
    public partial class PaymentWiseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PaymentWiseReport(CompanyDto companyDtl,List<PaymentWiseListLookupModel> paymentDtl)
        {
            InitializeComponent();
        }

    }
}
