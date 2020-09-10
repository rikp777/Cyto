using System;
using System.Collections.Generic;
using System.Globalization;
using DAL.Context;
using DAL.Repository.AuditTrail;
using Domain.Audit;
using Domain.Entities;

namespace LOGIC.Services.Audit_Trail
{
    public class AuditTrailService
    {
        private readonly UserAuditTrailRepository _userAuditTrailRepository;
        public AuditTrailService(DatabaseContext context)
        {
            _userAuditTrailRepository = new UserAuditTrailRepository(context);
        }
        
        public bool Capture<TEntity>(
            UserEntity user, 
            CompanyEntity company, 
            PermissionEntity permission,
            AuditActionType auditAction,
            string serviceName,
            string methodName,
            TEntity entity,
            List<AuditTrailChangeLogEntity> changes
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
                
                ActionType = auditAction,
                ServiceName = serviceName,
                MethodName= methodName,
                MethodColor = "Red",
                
                IpAddress = null,
            };
            auditTrail.AuditTrailChangeLog = changes;

            _userAuditTrailRepository.Create(auditTrail);

            return true;
        }
    }
}