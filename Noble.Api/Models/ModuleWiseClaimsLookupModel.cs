using System;

namespace Noble.Api.Models
{
    public class ModuleWiseClaimsLookupModel
    {
        public string TokenName { get; set; }
        public string Token { get; set; }
        public Guid CompanyId { get; set; }
    }
}
