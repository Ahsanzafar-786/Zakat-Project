using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Noble.Report.Reports.Invoice
{
    public partial class benificaryreports : DevExpress.XtraReports.UI.XtraReport
    {
        public benificaryreports(CompanyDto companydtl,List<BenificariesLookupModel> charity,string fromdate,string todate)
        {
            InitializeComponent();
            CompanyInfo.DataSource=companydtl;
            charity.ForEach(x =>
            {
                var authorizationPersonNames = x.BenificaryAuthorization
                    .Select(z => z.AuthorizationPersonName)
                    .Where(name => !string.IsNullOrEmpty(name));
                x.PassportNo = string.Join(",", authorizationPersonNames);
            });

            Beneficries.DataSource=charity;
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
