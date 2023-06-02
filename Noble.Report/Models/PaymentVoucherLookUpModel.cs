using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class PaymentVoucherLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Dates { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public string BillsName { get; set; }
        public decimal Amount { get; set; }
        public decimal Vatamount { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? BillsId { get; set; }
        public string PurchaseInvoiceCode { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public string SaleInvoiceCode { get; set; }
        public Guid BankCashAccountId { get; set; }
        public string BankCashAccountName { get; set; }
        public Guid ContactAccountId { get; set; }
        public string ContactAccountName { get; set; }
        public string ContactAccountNameAr { get; set; }
        public string DraftBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectBy { get; set; }
        public string VoidBy { get; set; }
        public string Reason { get; set; }
        public DateTime? DraftDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public string Image { get; set; }
        public string TaxMethod { get; set; }
        public string VatName { get; set; }
        public decimal? Rate { get; set; }
        public decimal? CurrentBalance { get; set; }
        public string runningBalance { get; set; }
        public string closingBalance { get; set; }

        public string QRCode { get; set; }
    }
}