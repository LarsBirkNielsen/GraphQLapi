using GraphQL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditLibrary.Configuration
{
     public class ConfigurationModel
    {
        public List<TablesModel> ExcludedTables { get; set; } = new();
        public List<AuditType> ActionsToLog { get; set; } = new();

        public static ConfigurationModel InitializeConfig()
        {
            var test = new ConfigurationModel();

            var tableModel = new TablesModel();
            tableModel.Name = "Author";


            test.ExcludedTables.Add(tableModel);

            Console.WriteLine("LOOK HERE: " + test.ExcludedTables);




            return test;
        }

    }
}