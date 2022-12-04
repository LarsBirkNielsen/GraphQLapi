using GraphQL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class ConfigurationModel
    {
        public List<TablesModel> Exclude { get; set; } = new();

        public List<String> ExcludeColumnsGlobally { get; set; } = new();
        public List<TablesModel> Include { get; set; } = new();
        public List<AuditType> ActionsToLog { get; set; } = new();

        public static ConfigurationModel InitializeConfig()
        {
            var config = new ConfigurationModel();

            //Only "Updates" gets logged
            config.ActionsToLog.Add(AuditType.Update);

            config.ExcludeColumnsGlobally.Add("Name");

            ////Table "Author" doesen't get logged
            config.Exclude.Add(new TablesModel("Author"));

            ////In Book table, "Genre" column doesen't get logged
            config.Exclude.Add(new TablesModel("Book", new[] { "Genre" }));

            ////The "Name" column in the "Book" table doesen't get logged when we create a "Book"
            ////It's possible to choose specific tables where you want to log "Create"/"Delete"
            config.Include.Add(new TablesModel("Book", new[] { "Name" }, new[] { AuditType.Update }));

            return config;
        }
    }
}