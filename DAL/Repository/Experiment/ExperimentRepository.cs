using DAL.Context;
using DAL.Interfaces;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Experiment
{
    public class ExperimentRepository : GenericCrudRepository<ExperimentEntity>, IGenericCrudRepository<ExperimentEntity>
    {
        public ExperimentRepository(IDatabaseContext context) : base(context, context.Experiments)
        {
        }
    }
}