using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DAL.Context;
using DAL.Interfaces;
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
            _experimentService = new ExperimentService(new DatabaseContext());
        }

        public ExperimentController(IDatabaseContext context)
        {
            _experimentService = new ExperimentService(context);
        }


        [HttpGet]
        [Route("experiments")]
        public IHttpActionResult GetAll()
        {
            var results = _experimentService.GetAll();
            if (results.ToArray().Length == 0) return Ok("There are no experiments found");
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
        public IHttpActionResult Create(ExperimentRequest experimentRequest)
        {
            if (experimentRequest == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }


            var result = _experimentService.Create(experimentRequest, HttpContext.Current);
            return Ok(result);
        }

        [HttpPut]
        [Route("experiments/{id}")]
        public IHttpActionResult Update(int id, ExperimentRequest experimentRequest)
        {
            if (experimentRequest == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }

            var experiment = _experimentService.GetById(id);
            if (experiment == null) return NotFound();

            var result = _experimentService.Update(id, experimentRequest, HttpContext.Current);
            return Ok(result);
        }

        [HttpDelete]
        [Route("experiments/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _experimentService.Delete(id, HttpContext.Current);
            return Ok(result);
        }
    }
}