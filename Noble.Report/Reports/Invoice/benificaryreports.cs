using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;

namespace Noble.Report.Reports.Invoice
{
    public partial class benificaryreports : DevExpress.XtraReports.UI.XtraReport
    {
        public benificaryreports(CompanyDto companydtl,List<BenificariesLookupModel> charity,string fromdate,string todate)
        {
            InitializeComponent();
            CompanyInfo.DataSource=companydtl;
            Beneficries.DataSource=charity;
        }

    }
}
