using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Company
{
    public class CompanyProjectRepository : IGenericRelationshipRepository<ProjectEntity>
    {
        private readonly IDatabaseContext _context;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;
        private readonly DbSet<ProjectEntity> _dbSetProjects;

        public CompanyProjectRepository(IDatabaseContext context)
        {
            _context = context;
            _dbSetCompanies = context.Companies;
            _dbSetProjects = context.Projects;
        }

        public ProjectEntity Attach(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);
            var project = _dbSetProjects.Find(projectId);
            if (company == null || project == null)
            {
                return null;
            }

            company.Projects.Add(project);

            _context.SaveChanges();
            return project;
        }

        public ProjectEntity Detach(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);
            var project = company?.Projects?.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }

            company.Projects.Remove(project);

            _context.SaveChanges();
            return project;
        }

        public ProjectEntity GetById(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);
            
            

            return company?.Projects?.FirstOrDefault(p => p.Id == projectId);
        }

        public List<ProjectEntity> GetAll(int companyId)
        {
            var company = _dbSetCompanies.Find(companyId);
            return company?.Projects?.ToList();
        }
    }
}