using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.Experiment
{
    public class ExperimentAuditTrailRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;
        private readonly DbSet<AuditTrailEntity> _dbSetAuditTrails;

        public ExperimentAuditTrailRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetCompanies = context.Set<CompanyEntity>();
            this._dbSetAuditTrails = context.Set<AuditTrailEntity>();
        }

        public AuditTrailEntity GetById(int experimentId, int auditTrailId)
        {
            var audit = _dbSetAuditTrails
                .Where(x => x.Identifier == "Experiment")
                .Where(x => x.PrimaryKey == experimentId.ToString())
                .Where(x =>x.Id == auditTrailId)
                .Include(x => x.Company)
                .Include(x => x.User)
                .First(x => x.Id == auditTrailId);
            return audit;
        }

        public List<AuditTrailEntity> GetAll(int experimentId)
        {
            var audits = _dbSetAuditTrails
                .Where(x => x.Identifier == "Experiment")
                .Where(x => x.PrimaryKey == experimentId.ToString())
                .Include(x => x.Company)
                .Include(x => x.User);

            return audits.ToList();
        }
    }
}