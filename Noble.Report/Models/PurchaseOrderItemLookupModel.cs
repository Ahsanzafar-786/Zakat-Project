﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noble.Report.Models
{
    public class PurchaseOrderItemLookupModel
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public string Note { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid WareHouseId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsExpire { get; set; }
        public string TaxMethod { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SerialNo { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public bool Serial { get; set; }
        public decimal ReceiveQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public Guid? WarrantyTypeId { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal AfterDiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal IncludingVat { get; set; }
        public decimal InclusiveVat { get; set; }
        public bool IsService { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string ProductNameArabic { get; set; }
        public string StyleNumber { get; set; }
    }
}
