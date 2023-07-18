using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using Noble.Report.Models;
using System.Collections.Generic;
using System.Linq;

namespace Noble.Report.Reports.Invoice
{
    public partial class benificaries : DevExpress.XtraReports.UI.XtraReport
    {
        public benificaries (CompanyDto companydtl,BenificariesLookupModel charity,string Language)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companydtl;
            Beneficries.DataSource = charity;
            xrLabel72.Text = Language == "en" ? charity.PaymentTypeName  : charity.PaymentTypeNameAr;
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
