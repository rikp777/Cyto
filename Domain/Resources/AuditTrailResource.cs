using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Newtonsoft.Json;

namespace Domain.Resources
{
    public class AuditTrailResource
    {
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        
        public string CreatedAt { get; set; }
        public string RequestMethod { get; set; }
        public string RequestMethodColor { get; set; }
        public string RequestBaseUrl { get; set; }
        public string IpAddress { get; set; }
        
       
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AuditTrailChangeLogResource> Changes { get; set; }
        
        public static AuditTrailResource FromEntity(AuditTrailEntity entity)
        {
            return new AuditTrailResource()
            {
                UserName = entity.User.Name,
                CompanyName = entity.Company.Name,
                
                CreatedAt = entity.CreatedAt,
                
                RequestMethod = entity.RequestMethod,
                RequestMethodColor = entity.RequestMethodColor,
                RequestBaseUrl = entity.RequestBaseUrl,
                
                IpAddress = entity.IpAddress,

                Changes = entity.AuditTrailChangeLog.Select(AuditTrailChangeLogResource.FromEntity).ToList().Count > 0 ? entity.AuditTrailChangeLog.Select(AuditTrailChangeLogResource.FromEntity).ToList() : null
            };
        }
    }
}