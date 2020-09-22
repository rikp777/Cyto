using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audit;
using Domain.Contracts;

namespace Domain.Entities
{
    public class AuditTrailEntity : BaseEntity
    {
        public virtual UserEntity User { get; set; }
        public virtual CompanyEntity Company { get; set; }
        public virtual PermissionEntity Permission { get; set; }


        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public virtual ICollection<AuditTrailChangeLogEntity> AuditTrailChangeLog { get; set; }

        public string CreatedAt { get; set; }
        
        public string RequestBaseUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestMethodColor { get; set; } // Indication color 
        
        public string IpAddress { get; set; } // Ip address of user 

        public override string ToString()
        {
            return RequestBaseUrl + " " + RequestMethod;
        }
    }
}