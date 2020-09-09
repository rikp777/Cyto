using System.Data.Entity;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.AuditTrail
{
    public class UserAuditTrailRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<AuditTrailEntity> _dbSet;

        public UserAuditTrailRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSet = context.Set<AuditTrailEntity>();
        }
        
        
        public bool Create(AuditTrailEntity auditTrail)
        {
            _dbSet.Add(auditTrail);

            return Save();
        }
        
        
        
        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}