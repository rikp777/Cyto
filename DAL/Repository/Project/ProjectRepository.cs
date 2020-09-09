using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Project
{
    public class ProjectRepository : GenericCrudRepository<ProjectEntity>, IGenericCrudRepository<ProjectEntity>
    {
        public ProjectRepository(DatabaseContext context) : base(context)
        {
        }
    }
}