
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Company
{
    public class CompanyProjectRepository : IGenericRelationshipRepository<ProjectEntity>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;
        private readonly DbSet<ProjectEntity> _dbSetProjects;

        public CompanyProjectRepository(DatabaseContext context)
        {
            _context = context;
            _dbSetCompanies = context.Set<CompanyEntity>();
            _dbSetProjects = context.Set<ProjectEntity>();
        }
        
        public bool Attach(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);
            var project = _dbSetProjects.Find(projectId);

            company?.Projects.Add(project);

            return _context.Save();
        }

        public bool Detach(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);
            var project = _dbSetProjects.Find(projectId);

            company?.Projects.Remove(project);

            return _context.Save();
        }

        public ProjectEntity GetById(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);

            return company?.Projects.First(p => p.Id == projectId);
        }

        public List<ProjectEntity> GetAll(int companyId)
        {
            var company = _dbSetCompanies.Find(companyId);

            return company?.Projects.ToList();
        }
    }
}