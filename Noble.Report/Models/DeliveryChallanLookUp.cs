using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class DeliveryChallanLookUp
    {
        public Guid Id { get; set; }
        public bool IsService { get; set; }
        public bool IsDeliveryChallan { get; set; }
        public bool IsClose { get; set; }
        public bool IsReserved { get; set; }
        public bool BilingAddress { get; set; }
        public string CustomerAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string NationalAddress { get; set; }
        public string BillingAddress1 { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public string Dates { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string SaleOrderRegistrationNumber { get; set; }
        public string SaleInvoiceRegistrationNumber { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public virtual ICollection<DeliveryChallanItemLookUpModel> DeliveryChallanItems { get; set; }

    }
}