using System.Data.Entity;
using DAL.Context;
using DAL.Interfaces;
using Domain.Entities;

namespace DAL.Repository.AuditTrail
{
    public class AuditTrailRepository
    {
        private readonly IDatabaseContext _context;
        private readonly DbSet<UserEntity> _dbSetUsers;
        private readonly DbSet<AuditTrailEntity> _dbSetAuditTrails;

        public AuditTrailRepository(IDatabaseContext context)
        {
            this._context = context;
            this._dbSetUsers = context.Users;
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