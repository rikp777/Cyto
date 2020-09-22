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
    }
}