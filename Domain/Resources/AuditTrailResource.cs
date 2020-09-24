using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Newtonsoft.Json;

namespace Domain.Resources
{
    public class AuditTrailResource
    {
        public string CreatedAt { get; set; }
        public string RequestBaseUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestMethodColor { get; set; }
        public string Audit { get; set; }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string IpAddress { get; set; }
        
        
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AuditTrailChangeLogResource> Changes { get; set; }
        
        public static AuditTrailResource FromEntity(AuditTrailEntity entity)
        {
            return new AuditTrailResource()
            {
                CreatedAt = entity.CreatedAt,
                RequestBaseUrl = entity.RequestBaseUrl,
                RequestMethod = entity.RequestMethod,
                RequestMethodColor = entity.RequestMethodColor,
                Audit = "User: '" + entity.User.Name + 
                        "' from company: '" + entity.Company.Name + 
                        "' made an '" + entity.RequestMethod + 
                        "' request on '" + entity.RequestBaseUrl + 
                        "' with flag-color '" + entity.RequestMethodColor + 
                        "' and Ip address '" + entity.IpAddress + "'",
                
                Id = entity.PrimaryKey,
                Name = entity.Identifier,
                UserName = entity.User.Name,
                CompanyName = entity.Company.Name,
                IpAddress = entity.IpAddress,

                Changes = entity.AuditTrailChangeLog.Select(AuditTrailChangeLogResource.FromEntity).ToList().Count > 0 ? entity.AuditTrailChangeLog.Select(AuditTrailChangeLogResource.FromEntity).ToList() : null
            };
        }
    }
}