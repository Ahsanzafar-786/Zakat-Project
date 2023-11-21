using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class PaymentAuthorizePerson : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {

        public string Code { get; set; }
        public string DocumentName { get; set; }
        public decimal Amount { get; set; }
        public Guid? AuthorizePersonId { get; set; }
        public virtual AuthorizedPerson AuthorizedPerson { get; set; }
      
        public DateTime?  Date { get; set; }

        public virtual ICollection<PaymentAuthorizePersonItem> PaymentAuthorizePersonItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
