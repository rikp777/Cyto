using System.Web.Http;
using DAL.Context;
using LOGIC.Services.User;

namespace API.Controllers.User
{
    [RoutePrefix("api/users")]
    public class UserAuditTrailController  : ApiController
    {
        private readonly UserAuditTrailService _userAuditTrailService;

        public UserAuditTrailController()
        {
            _userAuditTrailService = new UserAuditTrailService(new DatabaseContext());
        }
        
        
        [HttpGet]
        [Route("{userId}/audittrails")]
        public IHttpActionResult GetAll(int userId)
        {
            var results = _userAuditTrailService.GetAll(userId);
            return Ok(results);
        }

        [HttpGet]
        [Route("{userId}/audittrails/{audittrailId}")]
        public IHttpActionResult GetById(int userId, int auditTrailId)
        {
            var result = _userAuditTrailService.GetById(userId, auditTrailId);
            return Ok(result);
        }
    }
}