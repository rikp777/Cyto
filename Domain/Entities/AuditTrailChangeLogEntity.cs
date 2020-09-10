using Domain.Contracts;

namespace Domain.Entities
{
    public class AuditTrailChangeLogEntity : BaseEntity
    {
        public string ColumnName { get; set; } 
        public string ValueBefore { get; set; }  
        public string ValueAfter { get; set; }
        public bool Changed { get; set; }

        public AuditTrailChangeLogEntity() { }
        public AuditTrailChangeLogEntity(string columnName, string valueBefore, string valueAfter)
        {
            Changed = valueBefore != valueAfter;
            if (!Changed) return;
            
            this.ValueBefore = valueBefore;
            this.ValueAfter = valueAfter;
            this.ColumnName = columnName;
        }
    }
}