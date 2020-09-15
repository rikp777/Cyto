using System.Collections.Generic;
using System.Web.Http.Results;
using API.Controllers.Company;
using Domain.Entities;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Company
{
    [TestFixture]
    public class CompanyProjectTest
    {
        [Test]
        public void GetAllProjectsOfCompany_GoodWeather()
        {
            var context = new TestContext();
            var controller = new CompanyProjectController(context);

            var company = new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"};

            var projectOne = new ProjectEntity()
                {Name = "Super-duper project", Description = "The most amazing project ever"};

            var projectTwo = new ProjectEntity()
            {
                Name = "Kind of messy project", Description = "Hmm, this one can definitely be improved"
            };
            company.Projects = new List<ProjectEntity>() {projectOne, projectTwo};

            context.Companies.Add(company);

            var res = controller.GetAll(1) as OkNegotiatedContentResult<List<ProjectResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("Super-duper project", res.Content[0].Name);
            Assert.AreEqual("Kind of messy project", res.Content[1].Name);
        }

        [Test]
        public void GetAllProjectsOfCompany_BadWeather()
        {
            var context = new TestContext();
            var controller = new CompanyProjectController(context);
            var res = controller.GetAll(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
            context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            
            var controllerTwo = new CompanyController(context);
            var resTwo = controllerTwo.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("MySuccessfulCompany", resTwo.Content.Name);

            res = controller.GetAll(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }
    }
}