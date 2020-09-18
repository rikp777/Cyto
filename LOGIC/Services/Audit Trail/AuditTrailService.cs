using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DAL.Context;
using DAL.Repository.AuditTrail;
using Domain.Audit;
using Domain.Entities;

namespace LOGIC.Services.Audit_Trail
{
    public class AuditTrailService
    {
        private readonly AuditTrailRepository _auditTrailRepository;
        private readonly DatabaseContext _context;
        public AuditTrailService(DatabaseContext context)
        {
            _auditTrailRepository = new AuditTrailRepository(context);
            _context = context;
        }

        // private List<AuditTrailChangeLogEntity> getChanges<TEntity>(TEntity entityOld, TEntity entityNew)
        // {
        //     // PropertyInfo[] properties = typeof(TEntity).GetProperties();
        //     // List<string> changes = new List<string>();
        //     //
        //     // foreach (PropertyInfo pi in properties)
        //     // {
        //     //     object value1 = typeof(TEntity).GetProperty(pi.Name).GetValue(entityOld, null);
        //     //     object value2 = typeof(TEntity).GetProperty(pi.Name).GetValue(entityNew, null);
        //     //
        //     //     if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
        //     //     {
        //     //         changes.Add(string.Format("Property {0} changed from {1} to {2}", pi.Name, value1, value2));
        //     //     }
        //     // }
        //     //
        //     //
        //     // return new List<AuditTrailChangeLogEntity>();
        // }
        //
        
        public bool Capture<TEntity>(UserEntity user, CompanyEntity company, TEntity entityOld, TEntity entityNew)
        {
            // List<AuditTrailChangeLogEntity> changes = getChanges(entityOld, entityNew);

            //if value entityNew filled action: create
            //if value entityOld & entityNew both filled action: update
            

            return true;
        }
        
        // public bool Capture<TEntity>(
        //     UserEntity user, 
        //     CompanyEntity company, 
        //     PermissionEntity permission ,
        //     AuditActionType auditAction,
        //     string serviceName,
        //     string methodName,
        //     string primaryKey,
        //     TEntity entity,
        //     List<AuditTrailChangeLogEntity> changes
        // )
        // {
        //     var auditTrail = new AuditTrailEntity()
        //     {
        //         Company = company, 
        //         User = user,
        //         Permission = permission,
        //
        //         TableName = entity.GetType().Name,
        //         PrimaryKey = primaryKey,
        //         AuditTrailChangeLog = null,
        //
        //         CreatedAt = new DateTime().ToString(CultureInfo.InvariantCulture),
        //         
        //         ActionType = auditAction,
        //         ServiceName = serviceName,
        //         MethodName= methodName,
        //         MethodColor = "Red",
        //         
        //         IpAddress = null,
        //     };
        //     auditTrail.AuditTrailChangeLog = changes;
        //
        //     _auditTrailRepository.Create(auditTrail);
        //
        //     return true;
        // }
    }
}