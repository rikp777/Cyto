using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Company
{
    public class CompanyRepository : GenericCrudRepository<CompanyEntity>, IGenericCrudRepository<CompanyEntity>
    {
        public CompanyRepository(IDatabaseContext context) : base(context, context.Companies)
        {
        }

        public new bool Update(int id, CompanyEntity entity)
        {
            var temp = DbSet.First(u => u.Id == id);
            if (entity.Name != null)
            {
                temp.Name = entity.Name;
            }

            if (entity.Description != null)
            {
                temp.Description = entity.Description;
            }

            // Context.Entry(temp).Property(x => x.Name).IsModified = true;
            // Context.Entry(temp).Property(x => x.Description).IsModified = true;
            Context.MarkAsModified(temp);
            return Context.Save();
        }

        public CompanyEntity GetByName(string name)
        {
            var data = DbSet.SingleOrDefault(company => company.Name == name);
            return data;
        }
    }
}