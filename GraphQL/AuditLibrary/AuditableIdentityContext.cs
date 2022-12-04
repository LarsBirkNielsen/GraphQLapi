using Configuration;
using GraphQL.AuditLibrary.Service;
using GraphQL.Enums;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

namespace GraphQL.AuditLibrary
{
    public abstract class AuditableIdentityContext : DbContext
    {
        ConfigurationModel _config;
        private readonly IUserService _userService;

        public AuditableIdentityContext(DbContextOptions options, IUserService userService) : base(options)
        {
            _config = ConfigurationModel.InitializeConfig();
            _userService = userService;
        }


        public DbSet<Audit> AuditLogs { get; set; }
        public virtual async Task<int> SaveChangesAsync()
        {
            OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnBeforeSaveChanges()
        {

            var userId = _userService.GetUserId();


            var includedActions = _config.ActionsToLog.ToList();
            var excludedColumnsGlobally = _config.ExcludeColumnsGlobally.ToList();


            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);

                //Getting The table name
                auditEntry.TableName = entry.Entity.GetType().Name;

                if (!_config.Exclude.Where(x => x.Name == auditEntry.TableName && x.Columns == null).Any())
                {
                    auditEntry.UserId = userId;
                    //auditEntry.ClickBatchId = clickBatchId;
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
                                if (includedActions.Contains(AuditType.Create))
                                {
                                    if (!_config.Exclude.Where(x => x.Name == auditEntry.TableName && x.Columns.Contains(propertyName)).Any())
                                    {
                                        if (!excludedColumnsGlobally.Contains(propertyName))
                                        {
                                            Console.WriteLine("EXCLUDE");
                                            auditEntry.AuditType = AuditType.Create;
                                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                                            Console.WriteLine("EXCLUDED CREATE");
                                        }
                                    }
                                }
                                if (_config.Include.Where(x => x.IncludeLogActions.Contains(AuditType.Create)).Any())
                                {
                                    if (_config.Include.Where(x => x.Name == auditEntry.TableName && x.Columns.Contains(propertyName)).Any())
                                    {
                                        auditEntry.AuditType = AuditType.Create;
                                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    }

                                }
                                break;
                            case EntityState.Deleted:
                                if (includedActions.Contains(AuditType.Delete) && includedActions != null)
                                {
                                    auditEntry.AuditType = AuditType.Delete;
                                }
                                break;
                            case EntityState.Modified:
                                if (includedActions.Contains(AuditType.Update))
                                {
                                    if (property.IsModified && !_config.Exclude.Where(x => x.Name == auditEntry.TableName && x.Columns.Contains(propertyName)).Any())
                                    {
                                        if (!excludedColumnsGlobally.Contains(propertyName))
                                        {
                                            auditEntry.ChangedColumns.Add(propertyName);
                                            auditEntry.AuditType = AuditType.Update;
                                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                                        }
                                    }
                                }
                                if (_config.Include.Where(x => x.IncludeLogActions.Contains(AuditType.Update)).Any())
                                {
                                    if (_config.Include.Where(x => x.Name == auditEntry.TableName && x.Columns.Contains(propertyName)).Any())
                                    {
                                        auditEntry.ChangedColumns.Add(propertyName);
                                        auditEntry.AuditType = AuditType.Update;
                                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    }
                                }
                                break;
                        }
                    }
                    // To remove empty NULL entries into the auditlog.
                    if (auditEntry.AuditType.Equals(AuditType.None))
                    {
                        auditEntries.Remove(auditEntry);
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