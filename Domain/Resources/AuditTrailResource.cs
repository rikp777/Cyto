using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Newtonsoft.Json;

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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AuditTrailChangeLogResource> Changes { get; set; }
        
        public static AuditTrailResource FromEntity(AuditTrailEntity entity)
        {
            return new AuditTrailResource()
            {
                CreatedAt = entity.CreatedAt,
                ActionType = entity.ActionType.ToString(),
                MethodName = entity.MethodName,
                MethodColor = entity.MethodColor,
                IpAddress = entity.IpAddress,
                Reason = entity.Reason,
                CompanyName = entity.Company.Name,
                UserName = entity.User.Name,
                Changes = entity.AuditTrailChangeLog.Select(AuditTrailChangeLogResource.FromEntity).ToList().Count > 0 ? entity.AuditTrailChangeLog.Select(AuditTrailChangeLogResource.FromEntity).ToList() : null
            };
        }
    }
}