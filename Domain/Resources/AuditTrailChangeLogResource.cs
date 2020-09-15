using Domain.Entities;

namespace Domain.Resources
{
    public class AuditTrailChangeLogResource
    {
        public string ColumnName { get; set; } 
        public string ValueBefore { get; set; }  
        public string ValueAfter { get; set; }
        
        public static AuditTrailChangeLogResource FromEntity(AuditTrailChangeLogEntity entity)
        {
            return new AuditTrailChangeLogResource()
            {
                ColumnName = entity.ColumnName,
                ValueBefore = entity.ValueBefore,
                ValueAfter = entity.ValueAfter
            };
        }
    }
}