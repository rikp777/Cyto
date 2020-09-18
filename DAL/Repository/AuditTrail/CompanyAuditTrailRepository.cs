using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.AuditTrail
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
            _dbSetAuditTrails = context.Set<AuditTrailEntity>();
        }

        public AuditTrailEntity GetById(int companyId, int auditTrailId)
        {
            var audits = _dbSetAuditTrails.ToList().Where(x => x.Company.Id == companyId).ToList();
            return audits.Find(x => x.Id == auditTrailId);
        }

        public List<AuditTrailEntity> GetAll(int companyId)
        {
            //var company = _dbSetCompanies.Find(companyId);

            var audits = _dbSetAuditTrails.ToList().Where(x => x.Company.Id == companyId).ToList();

            return audits;
        }
    }
}