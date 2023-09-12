using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.IO;

namespace Noble.Report.Reports.Invoice
{
    public partial class PaymentWiseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PaymentWiseReport(CompanyDto companyDtl, PaymentWiseOpeningClosingModel paymentDtl)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companyDtl;
            PaymentTransection.DataSource=paymentDtl;
            //if (companyDtl.Base64Logo != null && companyDtl.Base64Logo != "" && companyDtl.Base64Logo != string.Empty)
            //{
            //    byte[] footerData = Convert.FromBase64String(companyDtl.Base64Logo);
            //    MemoryStream Footerms = new MemoryStream(footerData);
            //    Bitmap FooterImg = new Bitmap(Footerms);
            //    xrPictureBox1.Image = FooterImg;
            //}
        }

    }
}
