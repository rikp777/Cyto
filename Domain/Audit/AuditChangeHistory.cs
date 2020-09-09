namespace Domain.Audit
{
    public class AuditChangeHistory
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }
}