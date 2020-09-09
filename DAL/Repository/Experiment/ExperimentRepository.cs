using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Experiment
{
    public class ExperimentRepository : GenericCrudRepository<ExperimentEntity>, IGenericCrudRepository<ExperimentEntity>
    {
        public ExperimentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}