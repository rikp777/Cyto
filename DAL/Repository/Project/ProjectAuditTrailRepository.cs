using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using Domain.Entities;

namespace DAL.Repository.Project
{
    public class ProjectAuditTrailRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;
        private readonly DbSet<AuditTrailEntity> _dbSetAuditTrails;

        public ProjectAuditTrailRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetCompanies = context.Set<CompanyEntity>();
            this._dbSetAuditTrails = context.Set<AuditTrailEntity>();
        }

        public AuditTrailEntity GetById(int projectId, int auditTrailId)
        {
            var audit = _dbSetAuditTrails
                .Where(x => x.Identifier == "Project")
                .Where(x => x.PrimaryKey == projectId.ToString())
                .Where(x =>x.Id == auditTrailId)
                .Include(x => x.Company)
                .Include(x => x.User)
                .First(x => x.Id == auditTrailId);
            return audit;
        }

        public List<AuditTrailEntity> GetAll(int projectId)
        {
            var audits = _dbSetAuditTrails
                .Where(x => x.Identifier == "Project")
                .Where(x => x.PrimaryKey == projectId.ToString())
                .Include(x => x.Company)
                .Include(x => x.User);

            return audits.ToList();
        }
    }
}