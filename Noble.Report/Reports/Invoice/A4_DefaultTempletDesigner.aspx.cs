using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.Web;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice;

namespace Noble.Report.Reports.Invoice
{
    public partial class A4_DefaultTempletDesigner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //  if (!IsPostBack)
            //ASPxReportDesigner1.OpenReport(new A4_DefaultTemplet(GetCompanyInfo.GetCompanyInfodetials("b980c679-2e9e-43ee-0246-08d9badea086", Application["Token"].ToString())));
        }
    }
}