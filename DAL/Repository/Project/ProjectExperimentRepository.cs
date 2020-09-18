using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.Project
{
    public class ProjectExperimentRepository : IGenericRelationshipRepository<ExperimentEntity>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<ProjectEntity> _dbSetProjects;
        private readonly DbSet<ExperimentEntity> _dbSetExperiments;

        public ProjectExperimentRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSetProjects = context.Set<ProjectEntity>();
            this._dbSetExperiments = context.Set<ExperimentEntity>();
        }


        public ExperimentEntity Attach(int projectId, int experimentId)
        {
            var project = _dbSetProjects.Find(projectId);
            var experiment = _dbSetExperiments.Find(experimentId);

            project?.Experiments.Add(experiment);
            
            _context.Save();
            return experiment;
        }

        public ExperimentEntity Detach(int projectId, int experimentId)
        {
            var project = _dbSetProjects.Find(projectId);
            var experiment = _dbSetExperiments.Find(experimentId);

            project?.Experiments.Remove(experiment);
            
            _context.Save();
            return experiment;
        }

        public ExperimentEntity GetById(int projectId, int experimentId)
        {
            var project = _dbSetProjects.Find(projectId);

            return project?.Experiments.First(e => e.Id == experimentId);
        }

        public List<ExperimentEntity> GetAll(int projectId)
        {
            var project = _dbSetProjects.Find(projectId);

            return project?.Experiments.ToList();
        }
    }
}