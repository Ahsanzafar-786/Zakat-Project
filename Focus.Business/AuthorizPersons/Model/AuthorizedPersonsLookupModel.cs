using System;

namespace Focus.Business.AuthorizPersons.Model
{
    public class AuthorizedPersonsLookupModel
    {
        public Guid Id { get; set; }
        public int AuthorizedPersonCode { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }
}
