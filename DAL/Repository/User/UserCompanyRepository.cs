using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.User
{
    public class UserCompanyRepository : IGenericRelationshipRepository<CompanyEntity>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserEntity> _dbSetUsers;
        private readonly DbSet<CompanyEntity> _dbSetCompanies;

        public UserCompanyRepository(DatabaseContext context)
        {
            _context = context;
            _dbSetUsers = context.Set<UserEntity>();
            _dbSetCompanies = context.Set<CompanyEntity>();
        }

        public CompanyEntity Attach(int userId, int companyId)
        {
            var user = _dbSetUsers.Find(userId);
            var company = _dbSetCompanies.Find(companyId);

            user?.Companies.Add(company);

            _context.Save();
            return company;
        }

        public CompanyEntity Detach(int userId, int companyId)
        {
            var user = _dbSetUsers.Find(userId);
            var company = _dbSetCompanies.Find(companyId);

            user?.Companies.Remove(company);

            _context.Save();
            return company;
        }

        public CompanyEntity GetById(int userId, int companyId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.Companies.First(c => c.Id == companyId);
        }

        public List<CompanyEntity> GetAll(int userId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.Companies.ToList();
        }
    }
}