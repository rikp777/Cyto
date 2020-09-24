using System.Web.Http;
using DAL.Context;
using LOGIC.Services.Experiment;

namespace API.Controllers.Experiment
{
    [RoutePrefix("api/experiments")]
    public class ExperimentAuditTrailController : ApiController
    {
        private readonly ExperimentAuditTrailService _experimentAuditTrailService;

        public ExperimentAuditTrailController()
        {
            _experimentAuditTrailService = new ExperimentAuditTrailService(new DatabaseContext());
        }


        [HttpGet]
        [Route("{experimentId}/audittrails")]
        public IHttpActionResult GetAll(int experimentId)
        {
            var results = _experimentAuditTrailService.GetAll(experimentId);
            return Ok(results);
        }

        [HttpGet]
        [Route("{experimentId}/audittrails/{audittrailId}")]
        public IHttpActionResult GetById(int experimentId, int auditTrailId)
        {
            var result = _experimentAuditTrailService.GetById(experimentId, auditTrailId);
            return Ok(result);
        }
    }
}