using System.Collections.Generic;

namespace Domain.Audit
{
    public class AuditChangeEntity
    {
        public string DateTimeStamp { get; set; }
        public AuditActionType AuditActionType { get; set; }
        public List<AuditChangeHistory> AuditChangeHistories { get; set; }

        public AuditChangeEntity()
        {
            AuditChangeHistories = new List<AuditChangeHistory>();
        }
    }
}