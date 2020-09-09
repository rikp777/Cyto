using Domain.Contracts;

namespace Domain.Entities
{
    public class AuditTrailChangeLogEntity : BaseEntity
    {
        public string ColumnName { get; set; } 
        public string ValueBefore { get; set; }  
        public string ValueAfter { get; set; }
    }
}