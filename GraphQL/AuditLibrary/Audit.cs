namespace GraphQL.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int CorrelationId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string? AffectedColumns { get; set; }
        public int PrimaryKey { get; set; }
    }
}
