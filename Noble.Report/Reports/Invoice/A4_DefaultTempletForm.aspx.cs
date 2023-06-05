using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.Data;
using System.Linq;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.Globalization;

namespace Noble.Report.Reports.Invoice
{

    public partial class A4_DefaultTempletForm : System.Web.UI.Page
    {
   

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var CompanyId = Request.QueryString["CompanyID"];
                if (Session["CompanyId"] == null && Session["CompanyId"] != CompanyId)
                {
                    var myData = GetToken.TokenValue(Guid.Parse(CompanyId));
                    string token = myData.Token;
                    var serverAddress = myData.TokenName;
                    var companyInfo = GetCompanyInfo.GetCompanyInfodetials(CompanyId, token, serverAddress);
                    var formName = Request.QueryString["formName"];
                    var pageNumber = Request.QueryString["pageNumber"];
                    var id = Request.QueryString["id"];
                    var searchTerm = Request.QueryString["searchTerm"];
                    var TrailBalanceAccount = GetPayment.GetPaymentDtl(id,token, serverAddress);
                    XtraReport BalanceSheetReport = new Noble.Report.Reports.Invoice.PaymentReport(companyInfo, TrailBalanceAccount);
                    ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}