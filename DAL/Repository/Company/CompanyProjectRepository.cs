using System;
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
        private readonly IDatabaseContext _context;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;
        private readonly DbSet<ProjectEntity> _dbSetProjects;

        public CompanyProjectRepository(IDatabaseContext context)
        {
            _context = context;
            _dbSetCompanies = context.Companies;
            _dbSetProjects = context.Projects;
        }

        public bool Attach(int companyId, int projectId)
        {
            var company = _dbSetCompanies.Find(companyId);
            var project = _dbSetProjects.Find(projectId);
            Console.WriteLine(company == null);
            Console.WriteLine(project == null);

            company?.Projects.Add(project);

            // _context.Entry(company).Collection<ProjectEntity>("Projects").IsModified = true;
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

            return company?.Projects?.First(p => p.Id == projectId);
        }

        public List<ProjectEntity> GetAll(int companyId)
        {
            var company = _dbSetCompanies.Find(companyId);
            return company?.Projects?.ToList();
        }
    }
}