using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice
{
    public partial class LedgerReport : DevExpress.XtraReports.UI.XtraReport
    {
        public LedgerReport(CompanyDto companyDtl, List<CharityTransactionLookupModel> charitydel,string fromTime,string toTime)
        {
            InitializeComponent();

            Company.DataSource = companyDtl;
            Charity.DataSource = charitydel;
            if (companyDtl.Base64Logo != null && companyDtl.Base64Logo != "" && companyDtl.Base64Logo != string.Empty)
            {
                byte[] footerData = Convert.FromBase64String(companyDtl.Base64Logo);
                MemoryStream Footerms = new MemoryStream(footerData);
                Bitmap FooterImg = new Bitmap(Footerms);
                xrPictureBox1.Image = FooterImg;
            }
        }

    }
}
