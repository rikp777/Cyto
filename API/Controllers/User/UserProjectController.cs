using System.Web.Http;
using LOGIC.Services.User;

namespace API.Controllers.User
{
    [RoutePrefix("api/users")]
    public class UserProjectController : ApiController
    {
        private readonly UserProjectService _userProjectService;

        public UserProjectController()
        {
            _userProjectService = new UserProjectService();
        }

        [HttpGet]
        [Route("{userId}/projects")]
        public IHttpActionResult GetAll(int userId)
        {
            var results = _userProjectService.GetAll(userId);
            return Ok(results);
        }

        [HttpGet]
        [Route("{userId}/projects/{projectId}")]
        public IHttpActionResult GetById(int userId, int projectId)
        {
            var results = _userProjectService.GetById(userId, projectId);
            return Ok(results);
        }

        [HttpPost]
        [Route("{userId}/projects")]
        public IHttpActionResult Attach(int userId, int projectId)
        {
            var results = _userProjectService.Attach(userId, projectId);
            return Ok(results);
        }

        [HttpDelete]
        [Route("{userId}/projects")]
        public IHttpActionResult Detach(int userId, int projectId)
        {
            var results = _userProjectService.Detach(userId, projectId);
            return Ok(results);
        }
    }
}