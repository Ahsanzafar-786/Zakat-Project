using System;

namespace Focus.Business.AuthorizPersons.Model
{
    public class AuthorizedPersonsLookupModel
    {
        public Guid Id { get; set; }
        public int AuthorizedPersonCode { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string PassportNo { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string IqamaNo { get; set; }
        public bool IsActive { get; set; }
    }
}
