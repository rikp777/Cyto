using System.Web.Http;
using Domain.Requests;
using LOGIC.Services.User;

namespace API.Controllers.User
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        private readonly UserService _userService;
        
        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        [Route("users")]
        public IHttpActionResult GetAll()
        {
            var results = _userService.GetAll(1, 1);
            return Ok(results);
        }

        [HttpGet]
        [Route("users/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("users")]
        public IHttpActionResult Create(UserRequest entity)
        {
            var result = _userService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("users/{id}")]
        public IHttpActionResult Update(int id, UserRequest entity)
        {
            var result = _userService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("users/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            return Ok(result);
        }
    }
}