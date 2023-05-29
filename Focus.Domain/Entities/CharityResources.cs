using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class CharityResources : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public int ChartiyId { get;set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public bool Business { get; set; }
        public bool IsActive { get; set; }
        public Guid? ContactPerson { get; set; }
    }
}
