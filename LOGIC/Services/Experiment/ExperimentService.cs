using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Experiment;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Experiment
{
    public class ExperimentService : IGenericCrudService<ExperimentResource, ExperimentRequest>
    {
        private readonly ExperimentRepository _experimentRepository;
        public ExperimentService()
        {
            _experimentRepository = new ExperimentRepository(new DatabaseContext());
        }

        public ExperimentResource GetById(int id) => ExperimentResource.FromEntity(_experimentRepository.GetById(id));
        public List<ExperimentResource> GetAll(int size, int page) =>_experimentRepository
            .GetAll().Skip(size * (page -1)).Take(size)
            .Select(ExperimentResource.FromEntity)
            .ToList();

        public bool Create(ExperimentRequest entity) => _experimentRepository.Create(ExperimentRequest.ToEntity(entity));
        public bool Update(int id, ExperimentRequest entity) => _experimentRepository.Update(id, ExperimentRequest.ToEntity(entity));
        public bool Delete(int id) => _experimentRepository.Delete(id);
    }
}