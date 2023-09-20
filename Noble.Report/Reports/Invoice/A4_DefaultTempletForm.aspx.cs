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
using DevExpress.Office.NumberConverters;
using System.IO;
using DevExpress.Data;
using static iTextSharp.text.pdf.AcroFields;
using DevExpress.XtraRichEdit.Import.Doc;

namespace Noble.Report.Reports.Invoice
{

    public partial class A4_DefaultTempletForm : System.Web.UI.Page
    {
        public bool flag = false;

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
                    var id = Request.QueryString["id"] == "00000000-0000-0000-0000-000000000000" || Request.QueryString["id"] == null?"": Request.QueryString["id"];
                    var searchTerm = Request.QueryString["searchTerm"];
                    string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
                    var Print = Request.QueryString["Print"];
                    if (formName == "Payment")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        var TrailBalanceAccount = GetPayment.GetPaymentDtl(id, token, serverAddress);
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Invoice.PaymentReport(companyInfo, TrailBalanceAccount);
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                    }
                    if (formName == "Expensereport")
                    {

                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            var Expense = GetExpense.GetExpenseDtl( token, serverAddress);
                            XtraReport ExpenseReport = new Noble.Report.Reports.Invoice.ExpenseReportTemp1(companyInfo, Expense);
                            ASPxWebDocumentViewer1.OpenReport(ExpenseReport);
                            ExpenseReport.DisplayName = "Expense Report"+DateTime.Now;

                    }
                    if (formName == "Expense")
                    {

                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        var summery = GetExpensAtomic.GetExpensAtomicDtl(token, serverAddress);
                        XtraReport ExpenseReport = new Noble.Report.Reports.Invoice.ExpenseReportTemp2(summery);
                        ASPxWebDocumentViewer1.OpenReport(ExpenseReport);
                        ExpenseReport.DisplayName = "Expense Summery Report" + DateTime.Now;

                    }
                    else if (formName == "LedgerReport")
                    {
                        var benificaryId = Request.QueryString["benificaryId"]==null?"": Request.QueryString["benificaryId"];
                        var month = Request.QueryString["month"]== "Invalid date"?"": Request.QueryString["month"];
                        var fromDate = Request.QueryString["fromDate"] == "Invalid date" ? "" : Request.QueryString["fromDate"];
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
                                row["Amount"] = item.Amount.ToString("N2");
                                dt.Rows.Add(row);
                            }
                            ASPxGridView1.DataSource = dt;
                            ASPxGridView1.DataBind();
                            ASPxGridView1.TotalSummary.Clear();
                            ASPxSummaryItem Amount = new ASPxSummaryItem();
                            Amount.FieldName = "Amount";
                            Amount.DisplayFormat = "{0:#,0.00}";
                            Amount.SummaryType = SummaryItemType.Sum;
                            Amount.ShowInColumn = "Amount";
                            ASPxGridView1.TotalSummary.Add(Amount);


                        }

                    }
                    else if (formName == "PaymentWiseReport")
                    {
                        var benificaryId = Request.QueryString["benificaryId"] == null|| Request.QueryString["benificaryId"] == "null" ? "" : Request.QueryString["benificaryId"];
                        var UserId = Request.QueryString["userId"] == "Invalid date" ? "" : Request.QueryString["userId"];
                        var fromDate = Request.QueryString["fromDate"];
                        var toDate = Request.QueryString["toDate"];
                        var PaymantWiseTransection = GetPaymentWiseTransection.GetPaymentWiseTransectionDtl(benificaryId, UserId, fromDate, toDate, token, serverAddress);

                        if (Print == "true")
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport report = new Noble.Report.Reports.Invoice.PaymentWiseReport(companyInfo,PaymantWiseTransection);
                            ASPxWebDocumentViewer1.OpenReport(report);
                                
                        }
                        else
                        {
                            flag = true;
                            ASPxLabel padding = new ASPxLabel();
                            padding.Text = "";
                            padding.Visible = true;
                            padding.Width = 50;
                            this.Controls.Add(padding);
                            ASPxLabel Closing = new ASPxLabel();
                            Closing.Text = "Balance";
                            Closing.Visible = true;
                            Closing.Width = 900;
                            Closing.Font.Bold = true;
                            Closing.ForeColor = System.Drawing.ColorTranslator.FromHtml("#666");
                            this.Controls.Add(Closing);
                            ASPxLabel total = new ASPxLabel();
                            total.Text = PaymantWiseTransection.ClosingBalance.ToString("N2");
                            total.ForeColor = System.Drawing.ColorTranslator.FromHtml("#666");
                            total.Visible = true;
                            total.Font.Bold = true;
                            this.Controls.Add(total);


                            ASPxWebDocumentViewer1.Visible = false;
                            ASPxGridView1.Visible = true;

                            var dt = new DataTable();
                            dt.Columns.Add("#");
                            dt.Columns.Add("PaymentId");
                            dt.Columns.Add("BeneficaryId");
                            dt.Columns.Add("BeneficaryName");
                            dt.Columns.Add("CashierName");
                            dt.Columns.Add("PaymentType");
                            dt.Columns.Add("PaymentDate");
                            dt.Columns.Add("Amount");

                            DataRow row;
                            int i = 1;
                            row = dt.NewRow();
                            row["#"] = "";
                            row["PaymentId"] = "Opening Balance";
                            row["BeneficaryId"] = " - ";
                            row["BeneficaryName"] = " - ";
                            row["CashierName"] = " - ";
                            row["PaymentType"] = " - ";
                            row["PaymentDate"] = PaymantWiseTransection.OpeningBalance.ToString("N2");
                            row["Amount"] ="" ;
                            dt.Rows.Add(row);
                            foreach (var item in PaymantWiseTransection.PaymentList)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["PaymentId"] = item.PaymentId;
                                row["BeneficaryId"] = item.BeneficaryId;
                                row["BeneficaryName"] = item.BeneficaryName;
                                row["CashierName"] = item.CashierName;
                                row["PaymentType"] = item.PaymentType;
                                var selectedDate = "";
                                int j= 0;
                                    foreach (var item1 in item.SelectedMonth)
                                    {
                                    
                                        selectedDate +=  Convert.ToDateTime(item.SelectedMonth[j++]).ToString("MMMM yyyy")+"  ,";
                                    }
                                
                                row["PaymentDate"] = selectedDate;
                                row["Amount"] = item.Amount.ToString("N2");
                                dt.Rows.Add(row);
                            }

                            ASPxGridView1.DataSource = dt;
                            ASPxGridView1.DataBind();
                            ASPxGridView1.TotalSummary.Clear();
                            ASPxSummaryItem Amount = new ASPxSummaryItem();
                            Amount.FieldName = "Amount";
                            Amount.DisplayFormat = "{0:#,0.00}";
                            Amount.SummaryType = SummaryItemType.Sum;
                            Amount.ShowInColumn = "Amount";
                            ASPxGridView1.TotalSummary.Add(Amount);
                            ASPxGridView1.TotalSummary.Clear();
                            ASPxSummaryItem PaymentId = new ASPxSummaryItem();
                            Amount.FieldName = "PaymentId";
                            Amount.DisplayFormat = "Total";
                            Amount.ShowInColumn = "PaymentId";
                            ASPxGridView1.TotalSummary.Add(PaymentId);


                        }

                    }
                    else if (formName == "transactionreport")
                    {

                        var fromDate = Request.QueryString["fromDate"];
                        var toDate = Request.QueryString["toDate"];

                        var Transection = GetTransection.GetTransectionDtl(fromDate, toDate, token, serverAddress);

                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                         XtraReport report = new Noble.Report.Reports.Invoice.transectionReport(companyInfo, Transection,Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate));
                          ASPxWebDocumentViewer1.OpenReport(report);


                    }
                    else if (formName == "charityfundsreports")
                    {

                        var fromDate = Request.QueryString["fromDate"];
                        var toDate = Request.QueryString["toDate"];

                        var Transection = GetTransection.GetTransectionDtl(fromDate, toDate, token, serverAddress);

                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report = new Noble.Report.Reports.Invoice.charityfundsreports(companyInfo, Transection, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(report);


                    }
                    else if (formName == "benificaryreports")
                    {
                        var AuthorizationPersonId = Request.QueryString["AuthorizationPersonId"]== "null"?"" : Request.QueryString["AuthorizationPersonId"];
                        var BeneficiaryID = Request.QueryString["BeneficiaryID"] == "null"?"" : Request.QueryString["BeneficiaryID"];
                        var searchTerm1 = Request.QueryString["searchTerm"] == "null"?"" : Request.QueryString["searchTerm"];
                        var uqamaNo = Request.QueryString["uqamaNo"] == "null"?"" : Request.QueryString["uqamaNo"];
                        var ApprovalPersonId = Request.QueryString["ApprovalPersonId"] == "null" ? "" : Request.QueryString["ApprovalPersonId"];
                        var Registered = Request.QueryString["Registered"];
                        var fromDate = Request.QueryString["fromDate"];
                        var toDate = Request.QueryString["toDate"];
                        var Charity = GetBenificaryReport.GetBenificaryReportDtl(AuthorizationPersonId, ApprovalPersonId, Registered, uqamaNo, BeneficiaryID, searchTerm1, fromDate, toDate, token, serverAddress);

                            if (Print == "true")
                            {
                                ASPxWebDocumentViewer1.Visible = true;
                                ASPxGridView1.Visible = false;
                                XtraReport incomeStatementRpt = new Noble.Report.Reports.Invoice.benificaryreports(companyInfo, Charity, fromDate, toDate);
                                ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                                incomeStatementRpt.DisplayName = "VatPayableReport";
                            }
                            else
                            {
                                ASPxWebDocumentViewer1.Visible = false;
                                ASPxGridView1.Visible = true;
                                var dt = new DataTable();
                                dt.Columns.Add("#");
                                dt.Columns.Add("Id");
                                dt.Columns.Add("Name");
                                dt.Columns.Add("Type");
                                dt.Columns.Add("AutherizationPerson");
                                dt.Columns.Add("AmountPerMonth");
                                dt.Columns.Add("Reg/Un-Reg");

                                DataRow row;
                                int i = 1;
                                Charity.ForEach(x =>
                                {
                                    var authorizationPersonNames = x.BenificaryAuthorization
                                        .Select(z => z.AuthorizationPersonName)
                                        .Where(name => !string.IsNullOrEmpty(name));
                                    x.PassportNo = string.Join(",", authorizationPersonNames);
                                });

                                foreach (var item in Charity)
                                {
                                    row = dt.NewRow();
                                    row["#"] = i++;
                                    row["Id"] = item.BeneficiaryId;
                                    row["Name"] = item.Name;
                                    row["Type"] = item.DurationType;
                                    row["AutherizationPerson"] = item.PassportNo;
                                    row["AmountPerMonth"] = item.AmountPerMonth.ToString("N2");
                                    row["Reg/Un-Reg"] = item.IsRegister == true ? "Register" : "Un-Register";
                                    dt.Rows.Add(row);
                                }
                                ASPxGridView1.DataSource = dt;
                                ASPxGridView1.DataBind();
                            }
                    }
                   else if (formName == "benificary")
                    {
                        var AuthorizationPersonId = Request.QueryString["AuthorizationPersonId"] == "null"|| Request.QueryString["AuthorizationPersonId"] == "00000000-0000-0000-0000-000000000000" ? "" : Request.QueryString["AuthorizationPersonId"];
                        var Language = Request.QueryString["Language"] == "null" ? "" : Request.QueryString["Language"];
                        var Charity = GetBenificary.GetBenificaryDtl(AuthorizationPersonId, token, serverAddress);
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new Noble.Report.Reports.Invoice.benificaries(companyInfo, Charity, Language);
                            report1.CreateDocument();
                            using (var stream = new MemoryStream())
                            {
                                report1.ExportToPdf(stream);
                                byte[] bytes = stream.ToArray();
                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=Invoice.pdf");
                                Response.BinaryWrite(bytes);
                            }
                        }
                        else
                        {

                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport report = new Noble.Report.Reports.Invoice.benificaries(companyInfo, Charity, Language);
                            ASPxWebDocumentViewer1.OpenReport(report);
                        }
                    }
                    else if (formName == "Funds")
                    {
                        var Id = Request.QueryString["id"] == "null" || Request.QueryString["id"] == "00000000-0000-0000-0000-000000000000" ? "" : Request.QueryString["id"];
                        var funds = GetFunds.GetFundsDtl(Id, token, serverAddress);
                        if (Print=="true") {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport report1 = new Noble.Report.Reports.Invoice.Funds(companyInfo, funds);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = "FundReport";
                            ASPxWebDocumentViewer1.OpenReport(report1);
                        }
                        else
                        {

                            if (Convert.ToBoolean(isDownload))
                            {
                                // Enable CORS
                                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                                XtraReport report1 = new Noble.Report.Reports.Invoice.Funds(companyInfo, funds);
                                report1.CreateDocument();
                                using (var stream = new MemoryStream())
                                {
                                    report1.ExportToPdf(stream);
                                    byte[] bytes = stream.ToArray();
                                    Response.Clear();
                                    Response.ContentType = "application/pdf";
                                    Response.AppendHeader("Content-Disposition", "attachment; filename=Invoice.pdf");
                                    Response.BinaryWrite(bytes);
                                }
                            }
                            else
                            {

                                ASPxWebDocumentViewer1.Visible = true;
                                ASPxGridView1.Visible = false;
                                XtraReport report = new Noble.Report.Reports.Invoice.Funds(companyInfo, funds);
                                ASPxWebDocumentViewer1.OpenReport(report);
                            }
                        }
                    }
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (flag)
            {
                if (e.RowType == GridViewRowType.Data)
                {
                    string paymentId = e.GetValue("PaymentId") as string;

                    if (paymentId == "Opening Balance")
                    {
                        e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                        e.Row.BorderWidth = 0;
                        e.Row.BorderColor = System.Drawing.Color.White;
                        e.Row.Font.Bold = true;
                    }
                }
            }
        }
    }
}