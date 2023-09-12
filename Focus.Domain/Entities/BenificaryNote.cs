using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Focus.Domain.Entities
{
    public class BenificaryNote : BaseEntity
    {
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public Guid? BenificaryId { get; set; }
        public bool IsActive { get; set; }

        public virtual Beneficiaries Beneficiaries { get; set; }
    }
}
