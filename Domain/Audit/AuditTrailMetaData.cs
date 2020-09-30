using Domain.Entities;

namespace Domain.Audit
{
    public class AuditTrailMetaData
    {
        public UserEntity User;
        public CompanyEntity Company;
        public PermissionEntity Permission = null;
        public string RequestBaseUrl;
        public string RequestMethod;
        public string RequestIpAddress;
        
        /// <summary>
         /// Optionally only needed when there is no license can also be given to the company 
         /// </summary>
        public bool License = false;
    }
}