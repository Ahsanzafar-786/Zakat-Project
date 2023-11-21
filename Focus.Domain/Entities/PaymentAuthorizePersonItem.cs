using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class PaymentAuthorizePersonItem : BaseEntity
    {

        public Guid? PaymentAuthorizePersonId { get; set; }
        public virtual PaymentAuthorizePerson PaymentAuthorizePerson { get; set; }

        public Guid? BenificayId { get; set; }
        public virtual Beneficiaries Beneficiaries { get; set; }
    }
}
