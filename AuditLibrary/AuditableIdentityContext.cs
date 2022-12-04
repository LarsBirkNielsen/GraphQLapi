using GraphQL.Models;

namespace GraphQL.Data
{
    public abstract class AuditableIdentityContext : DbContext
    {
        public AuditableIdentityContext(DbContextOptions options) : base(options)
        {
        }


        private List<string> tablesToAuditLog = File.ReadLines("../GraphQL/Columns.txt").ToList();

        public DbSet<Audit> AuditLogs { get; set; }
        public virtual async Task<int> SaveChangesAsync(string userId, int clickBatchId)
        {
            OnBeforeSaveChanges(userId, clickBatchId);
            var result = await base.SaveChangesAsync();
            return result;
        }
        private void OnBeforeSaveChanges(string userId, int clickBatchId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);

                //Getting The table name
                auditEntry.TableName = entry.Entity.GetType().Name;


                //foreach (var item in tablesToAuditLog)
                //{
                //    if (auditEntry.TableName.Equals(item))
                if(!tablesToAuditLog.Contains(auditEntry.TableName))
                    {
                        auditEntry.UserId = userId;
                        auditEntry.ClickBatchId = clickBatchId;
                        auditEntries.Add(auditEntry);

                        foreach (var property in entry.Properties)
                        {
                            string propertyName = property.Metadata.Name;
                            if (property.Metadata.IsPrimaryKey())
                            {
                                auditEntry.PrimaryKey = (int)property.CurrentValue;
                                continue;
                            }
                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    auditEntry.AuditType = Enums.AuditType.Create;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    break;
                                case EntityState.Deleted:
                                    auditEntry.AuditType = Enums.AuditType.Delete;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    break;
                                case EntityState.Modified:
                                    if (property.IsModified && !tablesToAuditLog.Contains(propertyName))
                                    {

                                                auditEntry.ChangedColumns.Add(propertyName);
                                                auditEntry.AuditType = Enums.AuditType.Update;
                                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    }
                                    break;
                            }
                        //}
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}