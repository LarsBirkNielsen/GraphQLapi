using GraphQL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class TablesModel
    {
        public string Name { get; set; }
        public string[] Columns { get; set; }

        public AuditType[] IncludeLogActions { get; set; }

        public TablesModel(string tableName, string[] columnsName = null, AuditType[] auditTypes = null)
        {
            this.Name = tableName;
            this.Columns = columnsName;
            this.IncludeLogActions = auditTypes;
        }

    }
}