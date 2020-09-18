using System.Web.Http;
using LOGIC.Services.User;

namespace API.Controllers.User
{
    [RoutePrefix("api/users")]
    public class UserCompanyController : ApiController
    {
        private readonly UserCompanyService _userCompanyService;

        public UserCompanyController()
        {
            _userCompanyService = new UserCompanyService();
        }

        [HttpGet]
        [Route("{userId}/companies")]
        public IHttpActionResult GetAll(int userId)
        {
            var results = _userCompanyService.GetAll(userId);
            return Ok(results);
        }

        [HttpGet]
        [Route("{userId}/companies/{companyId}")]
        public IHttpActionResult GetById(int userId, int companyId)
        {
            var results = _userCompanyService.GetById(userId, companyId);
            return Ok(results);
        }

        [HttpPost]
        [Route("{userId}/companies")]
        public IHttpActionResult Attach(int userId, int companyId)
        {
            var results = _userCompanyService.Attach(userId, companyId);
            return Ok(results);
        }

        [HttpDelete]
        [Route("{userId}/companies")]
        public IHttpActionResult Detach(int userId, int companyId)
        {
            var results = _userCompanyService.Detach(userId, companyId);
            return Ok(results);
        }
    }
}