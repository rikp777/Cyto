using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.Interfaces;
using Domain.Audit;
using Domain.Entities;

namespace DAL.Repository.AuditTrail
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly IDatabaseContext _context;
        private readonly string _identifier; 
        
        public AuditTrailRepository(IDatabaseContext context, string identifier)
        {
            this._context = context;
            this._identifier = identifier;
        }

        private string GetPrimaryKey(ObjectStateEntry state)
        {
            _context.SaveChanges();
            return state.EntityKey.EntityKeyValues[0].Value.ToString();
        }
        
        private string GetTableName(ObjectStateEntry state)
        {
            return _identifier;
        }
        
        private List<AuditTrailChangeLogEntity> GetChanges()
        {
            //_context.
           // var myObjectState= _context.ObjectStateManager.GetObjectStateEntry(myObject);
            //var modifiedProperties=myObjectState.GetModifiedProperties();
            
            var changes = new List<AuditTrailChangeLogEntity>();
            var modifiedEntities = _context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified)
                .ToList();

            if (modifiedEntities.Count <= 0) return changes;
            
            foreach (var change in modifiedEntities)
            {
                foreach(var columnName in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.GetDatabaseValues().GetValue<object>(columnName).ToString();
                    var currentValue = change.CurrentValues[columnName].ToString();
                    if (originalValue != currentValue)
                    {
                        var auditChangeLog = new AuditTrailChangeLogEntity
                        {
                            ColumnName = columnName,
                            ValueBefore = originalValue, 
                            ValueAfter = currentValue
                        };
                        changes.Add(auditChangeLog);
                        
                        Console.WriteLine(@"Audit Trail: 'Value Changed': [Original value: " + originalValue + @"] - [Current value: " + currentValue + @"] - [Column name: " + columnName + @"]");
                    }
                }
            }
            return changes;
        }

        public bool Capture(AuditTrailMetaData auditTrailMetaData)
        {
            // Meta Data
            var user = auditTrailMetaData.User;
            var company = auditTrailMetaData.Company;
            var permission = auditTrailMetaData.Permission;
            var requestMethod = auditTrailMetaData.RequestMethod;
            var requestBaseUrl = auditTrailMetaData.RequestBaseUrl;
            var requestIpAddress = auditTrailMetaData.RequestIpAddress;
            var license = auditTrailMetaData.License;

            if (license == false) license = company.Licenses.Any(x => x.LicenseType.Name == "CFR");

            var now = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            // Validation
            if (license == false) return false;
            if (user == null) throw new Exception("AuditTrail Error: User is mandatory");
            if (company == null) throw new Exception("AuditTrail Error: Company is mandatory");
            if (permission == null) throw new Exception("AuditTrail Error: Permission is mandatory");
            if (requestIpAddress == null) requestIpAddress = "Not specified";
            if (requestMethod == null) requestMethod = "Not specified";
            if (requestBaseUrl == null) requestBaseUrl = "Not specified";
            
            
            var states =
                ((IObjectContextAdapter) _context).ObjectContext.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Added | EntityState.Modified | EntityState.Deleted);

            foreach (var state in states)
            {
                switch (state.State)
                {
                    case EntityState.Added:
                    {
                        var auditTrailEntity = new AuditTrailEntity()
                        {
                            User = user,
                            Company = company,
                            Permission = permission,
                            
                            RequestBaseUrl = requestBaseUrl,
                            RequestMethod = requestMethod,
                            RequestMethodColor = "Green",
                            IpAddress = requestIpAddress,

                            Identifier = GetTableName(state),
                            PrimaryKey = GetPrimaryKey(state),
                
                            CreatedAt = now
                        };
                        _context.Set<AuditTrailEntity>().Add(auditTrailEntity);
                        return true;
                    }
                    case EntityState.Modified:
                    {
                        var logs = GetChanges();
                        if (logs.Count == 0) return false;
                        var auditTrailUpdate = new AuditTrailEntity()
                        {
                            User = user,
                            Company = company,
                            Permission = permission,
                
                            RequestBaseUrl = requestBaseUrl,
                            RequestMethod = requestMethod,
                            RequestMethodColor = "Yellow",
                            IpAddress = requestIpAddress,
                            
                            AuditTrailChangeLog = logs,

                            Identifier = GetTableName(state),
                            PrimaryKey = GetPrimaryKey(state),
                            
                            CreatedAt = now.ToString(CultureInfo.InvariantCulture),
                        };
                       
                        _context.Set<AuditTrailEntity>().Add(auditTrailUpdate);
                        return true;
                    }
                    case EntityState.Deleted:
                    {
                        var auditTrailEntity = new AuditTrailEntity()
                        {
                            User = user,
                            Company = company,
                            Permission = permission,
                            
                            RequestBaseUrl = requestBaseUrl,
                            RequestMethod = requestMethod,
                            RequestMethodColor = "Red",
                            IpAddress = requestIpAddress,

                            Identifier = GetTableName(state),
                            PrimaryKey = GetPrimaryKey(state),
                
                            CreatedAt = now
                        };
                        _context.Set<AuditTrailEntity>().Add(auditTrailEntity);
                        return true;
                    }
                    case EntityState.Detached:
                    {
                        break;
                    }
                    case EntityState.Unchanged:
                    {
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return false;
        }
    }
}