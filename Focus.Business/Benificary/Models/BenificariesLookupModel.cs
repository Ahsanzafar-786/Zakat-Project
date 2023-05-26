using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Benificary.Models
{
    public class BenificariesLookupModel
    {
        public Guid Id { get; set; }
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public int PaymentIntervalMonth { get; set; }
        public decimal RecurringAmount { get; set; }
        public Guid? AuthorizedPersonId { get; set; }
        public decimal AmountPerMonth { get; set; }
        public string UgamaNo { get; set; }
        public string PhoneNo { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsRegister { get; set; }
        
        public string AuthorizationPersonName { get; set; }
    }
}
