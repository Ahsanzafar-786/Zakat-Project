using System;

namespace Focus.Business.PaymentsType.Model
{
    public class PaymentTypeLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
