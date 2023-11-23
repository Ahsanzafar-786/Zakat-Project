using System;
using Focus.Business.Benificary.Models;
using System.Collections.Generic;


namespace Focus.Business.PaymentByAuthPerson
{
    public class PaymentByAuthorizeLookUpModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string AuthorizePersonName { get; set; }
        public string AuthorizePersonNameAr { get; set; }
        public string UserId { get; set; }
        public DateTime? DateTime { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public ICollection<BenificariesLookupModel> Benificarylist { get; set; }
        public decimal Amount { get;  set; }
        public int AuthorizedPersonCode { get;  set; }
    }
}
