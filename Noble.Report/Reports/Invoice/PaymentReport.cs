using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice
{
    public partial class PaymentReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PaymentReport(CompanyDto companyDetail,PaymentLookupModel Payments)
        {
            InitializeComponent();

            PaymentDtl.DataSource = Payments;
            CompanyDtl.DataSource = companyDetail;
            //xrLabel17.Text=Convert.ToDateTime(Payments.Month).ToString("MMMM");

            //if (companyDetail.Base64Logo != null && companyDetail.Base64Logo != "" && companyDetail.Base64Logo != string.Empty)
            //{
            //    byte[] footerData = Convert.FromBase64String(companyDetail.Base64Logo);
            //    MemoryStream Footerms = new MemoryStream(footerData);
            //    Bitmap FooterImg = new Bitmap(Footerms);
            //    xrPictureBox1.Image = FooterImg;
            //}
        }

    }
}
