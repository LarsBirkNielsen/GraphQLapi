using GraphQL.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Models
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string UserId { get; set; }
        public int CorrelationId { get; set; }
        public int PrimaryKey { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.UserId = UserId;
            audit.CorrelationId = CorrelationId;
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.TimeOfUpdate = DateTime.Now;
            audit.PrimaryKey = PrimaryKey;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }

        //public Audit FromAudit()
        //{
        //    var audit = new Audit();
        //    audit.UserId = UserId;
        //    audit.ClickBatchId = ClickBatchId;
        //    audit.Type = AuditType.ToString();
        //    audit.TableName = TableName;
        //    audit.TimeOfUpdate = DateTime.Now;
        //    audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
        //    audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
        //    audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
        //    audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
        //    return audit;
        //}
    }
}
