// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using DAL.Context;
// using DAL.Repository.AuditTrail;
// using Domain.Audit;
// using Domain.Entities;
//
// namespace LOGIC.Services.Audit_Trail
// {
//     public class AuditTrailService
//     {
//         private readonly AuditTrailRepository _auditTrailRepository;
//         public AuditTrailService(DatabaseContext context)
//         {
//             _auditTrailRepository = new AuditTrailRepository(context);
//         }
//         
//         public bool Capture<TEntity>(
//             UserEntity user, 
//             CompanyEntity company, 
//             PermissionEntity permission,
//             AuditActionType auditAction,
//             string serviceName,
//             string methodName,
//             string primaryKey,
//             TEntity entity,
//             List<AuditTrailChangeLogEntity> changes
//         )
//         {
//             var auditTrail = new AuditTrailEntity()
//             {
//                 Company = company, 
//                 User = user,
//                 Permission = permission,
//
//                 TableName = entity.GetType().Name,
//                 PrimaryKey = primaryKey,
//                 AuditTrailChangeLog = null,
//
//                 CreatedAt = new DateTime().ToString(CultureInfo.InvariantCulture),
//                 
//                 ActionType = auditAction,
//                 ServiceName = serviceName,
//                 MethodName= methodName,
//                 MethodColor = "Red",
//                 
//                 IpAddress = null,
//             };
//             auditTrail.AuditTrailChangeLog = changes;
//
//             _auditTrailRepository.Create(auditTrail);
//
//             return true;
//         }
//     }
// }