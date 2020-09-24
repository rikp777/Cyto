using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.Company
{
    public class CompanyAuditTrailRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;
        private readonly DbSet<AuditTrailEntity> _dbSetAuditTrails;

        public CompanyAuditTrailRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetCompanies = context.Set<CompanyEntity>();
            this._dbSetAuditTrails = context.Set<AuditTrailEntity>();
        }

        public AuditTrailEntity GetById(int companyId, int auditTrailId)
        {
            var audit = _dbSetAuditTrails
                .Where(x => x.Company.Id == companyId)
                .Include(x => x.Company)
                .Include(x => x.User)
                .First(x => x.Id == auditTrailId);
            return audit;
        }

        public List<AuditTrailEntity> GetAll(int companyId)
        {
            var audits = _dbSetAuditTrails
               .Where(a => a.Company.Id == companyId)
               .Include(x => x.Company)
               .Include(x => x.User);

            return audits.ToList();
        }
    }
}