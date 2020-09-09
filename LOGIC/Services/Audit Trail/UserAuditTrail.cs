using System;
using System.Globalization;
using DAL.Context;
using DAL.Migrations;
using DAL.Repository.AuditTrail;
using Domain.Entities;
using Domain.Resources;

namespace LOGIC.Services.Audit_Trail
{
    public class UserAuditTrail
    {
        private readonly UserAuditTrailRepository _userAuditTrailRepository;
        public UserAuditTrail()
        {
            _userAuditTrailRepository = new UserAuditTrailRepository(new DatabaseContext());
        }
        
        public AuditTrailResource GetById(int userId)
        {
            return null;
        }


        public bool Capture<TEntity>(
            UserEntity user, 
            CompanyEntity company, 
            PermissionEntity permission,
            string serviceName,
            string methodName,
            ref TEntity entity
        )
        {
            var auditTrail = new AuditTrailEntity()
            {
                Company = company, 
                User = user,
                Permission = permission,

                TableName = entity.GetType().Name,
                AuditTrailChangeLog = null,

                CreatedAt = new DateTime().ToString(CultureInfo.InvariantCulture),
                
                ServiceName = serviceName,
                MethodName= methodName,
                MethodColor = "Red",
                
                IpAddress = null,
            };

            return true;
        }
    }
}