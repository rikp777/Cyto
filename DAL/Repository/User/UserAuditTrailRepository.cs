using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.User
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
            var audit = _dbSetAuditTrails
                .Where(x => x.User.Id == userId)
                .Include(x => x.Company)
                .Include(x => x.User)
                .First(x => x.Id == auditTrailId);
            return audit;
        }

        public List<AuditTrailEntity> GetAll(int userId)
        {
            var audits = _dbSetAuditTrails
                .Where(a => a.User.Id == userId)
                .Include(x => x.Company)
                .Include(x => x.User);
            return audits.ToList();
        }
    }
}