using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public abstract class BaseEntity:IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
