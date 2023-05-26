using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Interface
{
    public interface IApplicationDbContext
    {
       
        DbSet<Company> Companies { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<Beneficiaries> Beneficiaries { get; set; }
        DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }

        bool DisableTenantFilter { get; set; }
        int SaveChanges();
        int SaveChangesAfter();
        void SetModified(object entity, string attribute, Guid value);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
