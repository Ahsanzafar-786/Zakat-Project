using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Noble.Report.Models
{
    public class PurchaseOrderLookupModel
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Note { get; set; }
        public bool IsClose { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public string Path { get; set; }
        public bool IsRaw { get; internal set; }
        public virtual ICollection<PurchaseOrderItemLookupModel> PurchaseOrderItems { get; set; }
        public string TaxRatesName { get; set; }
        public string SupplierName { get; set; }
        public string QrCode { get; set; }
        public Guid? AdvanceAccountId { get; set; }
        public string Version { get; set; }
        public decimal ExpenseUse { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }

    }
}
