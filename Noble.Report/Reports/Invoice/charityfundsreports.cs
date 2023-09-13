using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice
{
    public partial class charityfundsreports : DevExpress.XtraReports.UI.XtraReport
    {
        public charityfundsreports(CompanyDto companyDtl, PaymentWiseOpeningClosingModel transectionInfo, DateTime fromtime, DateTime totime)
        {
            InitializeComponent();
            Company.DataSource = companyDtl;
            Transection.DataSource = transectionInfo;
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
