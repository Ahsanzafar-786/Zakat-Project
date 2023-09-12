using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
