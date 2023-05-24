using System;
using System.Reflection;
using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Focus.Persistence.Extensions
{
    public static class ChangeTracker
    {
        public static void SetShadowProperties(this Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker changeTracker, IUserHttpContextProvider userHttpContextProvider)
        {
            changeTracker.DetectChanges();
            var timestamp = DateTime.UtcNow;
            foreach (var entry in changeTracker.Entries())
            {
                if (entry.Entity is IAuditedEntityBase)
                {
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        entry.Property("ModifiedOn").CurrentValue = timestamp;
                        //TODO: Don't get ID if the table is Tenant
                        entry.Property("ModifiedById").CurrentValue = userHttpContextProvider.GetUserId().ToString();
                    }
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedOn").CurrentValue = timestamp;
                        //TODO: Don't get ID if the table is Tenant
                        entry.Property("CreatedById").CurrentValue = userHttpContextProvider.GetUserId().ToString();
                    
                    }
                }
                if (entry.Entity is ITenant)
                {
                    entry.Property("CompanyId").CurrentValue = userHttpContextProvider.GetCompanyId();
                }
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsDeleted").CurrentValue = true;
                }

                if (entry.Entity is IRecordDailyEntry)
                {
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        entry.Property("ClientId").CurrentValue = userHttpContextProvider.GetClientId();
                        entry.Property("BusinessId").CurrentValue = userHttpContextProvider.GetBusinessId();
                        entry.Property("UserId").CurrentValue = userHttpContextProvider.GetUserId();
                        entry.Property("CounterId").CurrentValue = userHttpContextProvider.GetCounterId();
                        entry.Property("DayId").CurrentValue = userHttpContextProvider.GetDayId();
                    }
                }
                
            }
        }

        private static void SetValue<T>(T obj, string propertyName, object value)
        {
            // these should be cached if possible
            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            pi.SetValue(obj, value);
        }
    }
}