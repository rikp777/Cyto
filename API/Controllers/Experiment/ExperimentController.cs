using System.Web.Http;
using Domain.Requests;
using LOGIC.Services.Experiment;

namespace API.Controllers.Experiment
{
    [RoutePrefix("api")]
    public class ExperimentController : ApiController
    {
        private readonly ExperimentService _experimentService;
        
        public ExperimentController()
        {
            _experimentService = new ExperimentService();
        }
        
        [HttpGet]
        [Route("Experiments")]
        public IHttpActionResult GetAll()
        {
            var results = _experimentService.GetAll(1, 1);
            return Ok(results);
        }

        [HttpGet]
        [Route("Experiments/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _experimentService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("Experiments")]
        public IHttpActionResult Create(ExperimentRequest entity)
        {
            var result = _experimentService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("Experiments/{id}")]
        public IHttpActionResult Update(int id, ExperimentRequest entity)
        {
            var result = _experimentService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Experiments/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _experimentService.Delete(id);
            return Ok(result);
        }
    }
}