using System.Data.Entity;
using DAL.Interfaces;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Company
{
    public class CompanyRepository : GenericCrudRepository<CompanyEntity>, IGenericCrudRepository<CompanyEntity>
    {
        public CompanyRepository(IDatabaseContext context) : base(context, context.Companies)
        {
        }
    }
}