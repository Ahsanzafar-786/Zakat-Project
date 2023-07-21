using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using DevExpress.Office.NumberConverters;
using System.Globalization;
using System.IO;

namespace Noble.Report.Reports.Invoice
{
    public partial class Funds : DevExpress.XtraReports.UI.XtraReport
    {
        public Funds (CompanyDto companydtl,FundsLookupModel funds)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companydtl;
            fundsInfo.DataSource = funds;
            if (companydtl.Base64Logo != null && companydtl.Base64Logo != "" && companydtl.Base64Logo != string.Empty)
            {
                byte[] footerData = Convert.FromBase64String(companydtl.Base64Logo);
                MemoryStream Footerms = new MemoryStream(footerData);
                Bitmap FooterImg = new Bitmap(Footerms);
                xrPictureBox1.Image = FooterImg;
            }
        }

    }
}
