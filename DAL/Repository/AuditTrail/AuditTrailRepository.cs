using System.Data.Entity;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.AuditTrail
{
    public class AuditTrailRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserEntity> _dbSetUsers;
        private readonly DbSet<AuditTrailEntity> _dbSetAuditTrails;

        public AuditTrailRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetUsers = context.Set<UserEntity>();
            _dbSetAuditTrails = context.Set<AuditTrailEntity>();
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