using System.Web.Http;
using Domain.Requests;
using LOGIC.Services.Company;

namespace API.Controllers.Company
{
    [RoutePrefix("api")]
    public class CompanyController : ApiController
    {
        private readonly CompanyService _companyService;
        
        public CompanyController()
        {
            _companyService = new CompanyService();
        }
        
        [HttpGet]
        [Route("companies")]
        public IHttpActionResult GetAll()
        {
            var results = _companyService.GetAll(1, 1);
            return Ok(results);
        }

        [HttpGet]
        [Route("companies/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _companyService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("companies")]
        public IHttpActionResult Create(CompanyRequest entity)
        {
            var result = _companyService.Create(entity);
            return Ok(result);
        }

        [HttpPut]
        [Route("companies/{id}")]
        public IHttpActionResult Update(int id, CompanyRequest entity)
        {
            var result = _companyService.Update(id, entity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("companies/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _companyService.Delete(id);
            return Ok(result);
        }
    }
}