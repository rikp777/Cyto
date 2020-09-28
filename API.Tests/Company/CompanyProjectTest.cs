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
        private TestContext _context;
        private CompanyProjectController _controller;

        [SetUp]
        public void Init()
        {
            _context = new TestContext();
            _controller = new CompanyProjectController(_context);
        }

        [Test]
        public void GetAllProjectsOfCompany_GoodWeather()
        {
            var company = new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"};

            _context.Projects.Add(new ProjectEntity()
                {Name = "Super-duper project", Description = "The most amazing project ever"});

            _context.Projects.Add(new ProjectEntity()
            {
                Name = "Kind of messy project", Description = "Hmm, this one can definitely be improved"
            });

            company.Projects = new List<ProjectEntity>() {_context.Projects.Find(1), _context.Projects.Find(2)};
            _context.Companies.Add(company);

            var res = _controller.GetAll(1) as OkNegotiatedContentResult<List<ProjectResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("Super-duper project", res.Content[0].Name);
            Assert.AreEqual("Kind of messy project", res.Content[1].Name);
        }

        [Test]
        public void GetAllProjectsOfCompany_BadWeather()
        {
            var res = _controller.GetAll(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});

            var controllerTwo = new CompanyController(_context);
            var resTwo = controllerTwo.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(resTwo);
            Assert.AreEqual("MySuccessfulCompany", resTwo.Content.Name);

            res = _controller.GetAll(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void GetProjectOfCompanyById_BadWeather()
        {
            var res = _controller.GetById(1, 1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});

            var controllerTwo = new CompanyController(_context);
            var resTwo = controllerTwo.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(resTwo);
            Assert.AreEqual("MySuccessfulCompany", resTwo.Content.Name);

            res = _controller.GetById(1, 1);


            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void GetProjectOfCompanyById_GoodWeather()
        {
            var company = new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"};

            var projectOne = new ProjectEntity()
                {Name = "Super-duper project", Description = "The most amazing project ever"};

            var projectTwo = new ProjectEntity()
            {
                Name = "Kind of messy project", Description = "Hmm, this one can definitely be improved"
            };

            _context.Projects.Add(projectOne);
            _context.Projects.Add(projectTwo);

            company.Projects = new List<ProjectEntity>() {_context.Projects.Find(1), _context.Projects.Find(2)};

            _context.Companies.Add(company);

            var res = _controller.GetById(1, 1) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Super-duper project", res.Content.Name);

            res = _controller.GetById(1, 2) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Kind of messy project", res.Content.Name);
        }

        [Test]
        public void AttachProjectToCompany_BadWeather()
        {
            var res = _controller.Attach(1, 1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);

            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});

            var controllerTwo = new CompanyController(_context);
            var resTwo = controllerTwo.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(resTwo);
            Assert.AreEqual("MySuccessfulCompany", resTwo.Content.Name);

            res = _controller.Attach(1, 1);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void AttachProjectToCompany_GoodWeather()
        {
            _context.Companies.Add(new CompanyEntity()
            {
                Name = "MySuccessfulCompany", Description = "Best company ever", Projects = new List<ProjectEntity>()
            });

            _context.Projects.Add(new ProjectEntity()
                {Name = "Super-duper project", Description = "The most amazing project ever"});

            _context.Projects.Add(new ProjectEntity()
            {
                Name = "Kind of messy project", Description = "Hmm, this one can definitely be improved"
            });
            var res = _controller.Attach(1, 1) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Super-duper project", res.Content.Name);

            res = _controller.GetById(1, 1) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Super-duper project", res.Content.Name);


            res = _controller.Attach(1, 2) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Kind of messy project", res.Content.Name);

            res = _controller.GetById(1, 2) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Kind of messy project", res.Content.Name);
        }

        [Test]
        public void DetachProjectFromCompany_BadWeather()
        {
            var res = _controller.Detach(1, 1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);

            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});

            var controllerTwo = new CompanyController(_context);
            var resTwo = controllerTwo.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(resTwo);
            Assert.AreEqual("MySuccessfulCompany", resTwo.Content.Name);

            res = _controller.Detach(1, 1);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void DetachProjectFromCompany_GoodWeather()
        {
            var company = new CompanyEntity()
            {
                Name = "MySuccessfulCompany", Description = "Best company ever", Projects = new List<ProjectEntity>()
            };

            _context.Projects.Add(new ProjectEntity()
                {Name = "Super-duper project", Description = "The most amazing project ever"});

            company.Projects = new List<ProjectEntity>() {_context.Projects.Find(1)};

            _context.Companies.Add(company);
            var res = _controller.GetById(1, 1) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Super-duper project", res.Content.Name);

            res = _controller.Detach(1, 1) as OkNegotiatedContentResult<ProjectResource>;
            Assert.IsNotNull(res);
            Assert.AreEqual("Super-duper project", res.Content.Name);

            var resTwo = _controller.GetById(1, 1);

            Assert.IsNotNull(resTwo);
            Assert.IsInstanceOf<NotFoundResult>(resTwo);
        }
    }
}