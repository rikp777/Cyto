using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DAL.Context;
using DAL.Migrations;
using DAL.Repository.AuditTrail;
using Domain.Audit;
using Domain.Entities;
using Domain.Resources;

namespace LOGIC.Services.Audit_Trail
{
    public class UserAuditTrailService
    {
        private readonly UserAuditTrailRepository _userAuditTrailRepository;
        public UserAuditTrailService(DatabaseContext context)
        {
            _userAuditTrailRepository = new UserAuditTrailRepository(context);
        }
        public List<AuditTrailResource> GetAll(int userId)
        {
            return _userAuditTrailRepository.GetAll(userId).Select(AuditTrailResource.FromEntity).ToList();
        }

        public AuditTrailResource GetById(int userId, int auditTrailId)
        {
            return AuditTrailResource.FromEntity(_userAuditTrailRepository.GetById(userId, auditTrailId));
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