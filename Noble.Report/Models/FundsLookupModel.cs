using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class FundsLookupModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public Decimal Amount { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
        public string CharityResouceName { get; set; }
        public Guid? CharityResouceId { get; set; }
        public string TypeOfTransaction { get; set; }
        public string Cashier { get; set; }
    }
}