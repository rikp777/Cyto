using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Company
{
    public class CompanyRepository : GenericCrudRepository<CompanyEntity>, IGenericCrudRepository<CompanyEntity>
    {
        public CompanyRepository(DatabaseContext context) : base(context)
        {
        }

        public new bool Update(int id, CompanyEntity entity)
        {
            var temp = _dbSet.First(u => u.Id == id);
            temp.Name = entity.Name;
            temp.Description = entity.Description;
            _context.Entry(temp).Property(x => x.Name).IsModified = true;
            _context.Entry(temp).Property(x => x.Description).IsModified = true;
            return Save();
        }

        public CompanyEntity GetByName(string name)
        {
            var data = _dbSet.SingleOrDefault(company => company.Name == name);
            return data;
        }
    }
}