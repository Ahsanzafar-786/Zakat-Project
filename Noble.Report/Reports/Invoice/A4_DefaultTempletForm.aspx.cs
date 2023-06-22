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
                    var Print = Request.QueryString["Print"];
                    if (formName == "Payment")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        var TrailBalanceAccount = GetPayment.GetPaymentDtl(id, token, serverAddress);
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Invoice.PaymentReport(companyInfo, TrailBalanceAccount);
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                    }
                    else if (formName == "LedgerReport")
                    {
                        var benificaryId = Request.QueryString["benificaryId"];
                        var month = Request.QueryString["month"]== "Invalid date"?"": Request.QueryString["month"];
                        var fromDate = Request.QueryString["fromDate"];
                        var toDate = Request.QueryString["toDate"];
                        var Charity = GetCharityLedger.GetCharityLedgerDtl(benificaryId,month,fromDate,toDate, token, serverAddress);
                        if (Print == "true")
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport incomeStatementRpt = new Noble.Report.Reports.Invoice.LedgerReport(companyInfo, Charity, fromDate, toDate);
                            ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                            incomeStatementRpt.DisplayName = "VatPayableReport";
                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = false;
                            ASPxGridView1.Visible = true;
                            var dt = new DataTable();
                            dt.Columns.Add("#");
                            dt.Columns.Add("DocumentCode");
                            dt.Columns.Add("DocumentDate");
                            dt.Columns.Add("CharityTransactionDate");
                            dt.Columns.Add("Month");
                            dt.Columns.Add("Year");
                            dt.Columns.Add("BenificaryName");
                            dt.Columns.Add("Amount");

                            DataRow row;
                            int i = 1;
                            foreach (var item in Charity)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["DocumentCode"] = item.DoucmentCode;
                                row["DocumentDate"] = item.DoucmentDate;
                                row["CharityTransactionDate"] = item.CharityTransactionDate;
                                row["Month"] = Convert.ToDateTime(item.Month).ToString("MMMM");
                                row["Year"] = item.Year;
                                row["BenificaryName"] = item.benificaryName;
                                row["Amount"] = item.Amount?.ToString("#.##");
                                dt.Rows.Add(row);
                            }
                            ASPxGridView1.DataSource = dt;
                            ASPxGridView1.DataBind();


                        }

                    }
                    else if (formName == "benificaryreports")
                    {
                        var benificaryId = Request.QueryString["benificaryId"];
                        var month = Request.QueryString["month"] == "Invalid date" ? "" : Request.QueryString["month"];
                        var fromDate = Request.QueryString["fromDate"];
                        var toDate = Request.QueryString["toDate"];
                        var Charity = GetCharityLedger.GetCharityLedgerDtl(benificaryId, month, fromDate, toDate, token, serverAddress);
                        if (Print == "true")
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport incomeStatementRpt = new Noble.Report.Reports.Invoice.LedgerReport(companyInfo, Charity, fromDate, toDate);
                            ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                            incomeStatementRpt.DisplayName = "VatPayableReport";
                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = false;
                            ASPxGridView1.Visible = true;
                            var dt = new DataTable();
                            dt.Columns.Add("#");
                            dt.Columns.Add("DocumentCode");
                            dt.Columns.Add("DocumentDate");
                            dt.Columns.Add("CharityTransactionDate");
                            dt.Columns.Add("Month");
                            dt.Columns.Add("Year");
                            dt.Columns.Add("BenificaryName");
                            dt.Columns.Add("Amount");

                            DataRow row;
                            int i = 1;
                            foreach (var item in Charity)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["DocumentCode"] = item.DoucmentCode;
                                row["DocumentDate"] = item.DoucmentDate;
                                row["CharityTransactionDate"] = item.CharityTransactionDate;
                                row["Month"] = Convert.ToDateTime(item.Month).ToString("MMMM");
                                row["Year"] = item.Year;
                                row["BenificaryName"] = item.benificaryName;
                                row["Amount"] = item.Amount?.ToString("#.##");
                                dt.Rows.Add(row);
                            }
                            ASPxGridView1.DataSource = dt;
                            ASPxGridView1.DataBind();


                        }

                    }

                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}