using System.Collections;
using System.Collections.Generic;
using Domain.Audit;
using Domain.Contracts;

namespace Domain.Entities
{
    public class AuditTrailEntity : BaseEntity
    {
        public CompanyEntity Company { get; set; }
        public UserEntity User{ get; set; }
        public PermissionEntity Permission { get; set; }

        public string TableName { get; set; }
        public virtual ICollection<AuditTrailChangeLogEntity> AuditTrailChangeLog { get; set; }

        public string CreatedAt { get; set; }
        
        public AuditActionType ActionType { get; set; } // Action Type description (CRUD)
        public string ServiceName { get; set; } //Controller name or service class name
        public string MethodName { get; set; } // function name 
        public string MethodColor { get; set; } // Indication color 
        
        public string IpAddress { get; set; } // Ip address of user 
        public string Reason { get; set; } // Why did the user wanted to do the change

        public override string ToString()
        {
            return ServiceName + " " + MethodName;
        }
    }
}