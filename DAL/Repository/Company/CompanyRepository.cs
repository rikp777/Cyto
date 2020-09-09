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
    }
}