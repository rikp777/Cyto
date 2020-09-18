using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Resources
{
    public class AuditTrailResource
    {
        public string CreatedAt { get; set; }
        public string ActionType { get; set; }
        public string MethodName { get; set; }
        public string MethodColor { get; set; }
        public string IpAddress { get; set; }
        public string Reason { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public ICollection<AuditTrailChangeLogEntity> Changes { get; set; }

        public static AuditTrailResource FromEntity(AuditTrailEntity entity)
        {
            AuditTrailResource resource = new AuditTrailResource();
            resource.CreatedAt = entity.CreatedAt;
            resource.ActionType = entity.ActionType.ToString();
            resource.MethodName = entity.MethodName;
            resource.MethodColor = entity.MethodColor;
            resource.IpAddress = entity.IpAddress;
            resource.Reason = entity.Reason;
            resource.Changes = entity.AuditTrailChangeLog;
            
            if (entity.Company != null) resource.CompanyName = entity.Company.Name;
            if (entity.User != null) resource.UserName = entity.User.Name;

            return resource;
        }
    }
}