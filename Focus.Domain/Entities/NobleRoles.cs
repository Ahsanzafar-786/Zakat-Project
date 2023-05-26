using Focus.Domain.Interface;


namespace Focus.Domain.Entities
{
    public class NobleRoles : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string NormalizedName { get; set; }
        public bool IsActive { get; set; }
    }
}
