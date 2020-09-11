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
        [Route("experiments")]
        public IHttpActionResult GetAll()
        {
            var results = _experimentService.GetAll();
            return Ok(results);
        }

        [HttpGet]
        [Route("experiments/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _experimentService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("experiments")]
        public IHttpActionResult Create(ExperimentRequest entity)
        {
            var result = _experimentService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("experiments/{id}")]
        public IHttpActionResult Update(int id, ExperimentRequest entity)
        {
            var result = _experimentService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("experiments/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _experimentService.Delete(id);
            return Ok(result);
        }
    }
}