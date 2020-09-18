using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.AuditTrail
{
    public class UserAuditTrailRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserEntity> _dbSetUsers;
        private readonly DbSet<AuditTrailEntity> _dbSetAuditTrails;

        public UserAuditTrailRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetUsers = context.Set<UserEntity>();
            _dbSetAuditTrails = context.Set<AuditTrailEntity>();
        }
        
        public AuditTrailEntity GetById(int userId, int auditTrailId)
        {
            var user = _dbSetUsers.Find(userId);

            _context.AuditTrails.Include(x => x.AuditTrailChangeLog);

            return user?.AuditTrails.First(a => a.Id == auditTrailId);
        }

        public List<AuditTrailEntity> GetAll(int userId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.AuditTrails.ToList();
        }

        public List<AuditTrailEntity> GetAll(
            Expression<Func<AuditTrailEntity, bool>> filter = null,
            Func<IQueryable<AuditTrailEntity>, IOrderedQueryable<AuditTrailEntity>> orderBy = null, 
            string includedProperties = "")
        {
            IQueryable<AuditTrailEntity> query = _dbSetAuditTrails;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includedProperties);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }
        
        
        public bool Create(AuditTrailEntity auditTrail)
        {
            _dbSetAuditTrails.Add(auditTrail);

            return Save();
        }
        
        
        
        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}