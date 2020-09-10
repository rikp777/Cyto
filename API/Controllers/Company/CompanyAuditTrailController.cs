using System.Web.Http;
using DAL.Context;
using LOGIC.Services.Audit_Trail;
using LOGIC.Services.Company;

namespace API.Controllers.Company
{
    [RoutePrefix("api/companies")]
    public class CompanyAuditTrailController : ApiController
    {

        private readonly CompanyAuditTrailService _companyAuditTrail;

        public CompanyAuditTrailController()
        {
            _companyAuditTrail = new CompanyAuditTrailService(new DatabaseContext());
        }
        
        [HttpGet]
        [Route("{companyId}/audittrails")]
        public IHttpActionResult GetAll(int companyId)
        {
            var results = _companyAuditTrail.GetAll(companyId);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("{companyId}/audittrails/{auditTrailId}")]
        public IHttpActionResult GetById(int companyId, int auditTrailId)
        {
            var results = _companyAuditTrail.GetById(companyId, auditTrailId);
            return Ok(results);
        }
    }
}