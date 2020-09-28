using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;
using API.Controllers.Company;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Company
{
    [TestFixture]
    public class CompanyTest : ITestSkeleton
    {
        private TestContext _context;
        private CompanyController _controller;

        [SetUp]
        public void Init()
        {
            _context = new TestContext();
            _controller = new CompanyController(_context);
        }

        [Test]
        public void GetAll_GoodWeather()
        {
            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            _context.Companies.Add(new CompanyEntity()
                {Name = "MyPoorCompany", Description = "Worst company ever"});
            var res = _controller.GetAll() as OkNegotiatedContentResult<List<CompanyResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("MySuccessfulCompany", res.Content[0].Name);
            Assert.AreEqual("MyPoorCompany", res.Content[1].Name);
        }

        [Test]
        public void GetAll_BadWeather()
        {
            var res = _controller.GetAll() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(res);
            Assert.AreEqual("There are no companies found", res.Content);
        }

        [Test]
        public void GetById_GoodWeather()
        {
            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            var res = _controller.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("MySuccessfulCompany", res.Content.Name);
        }

        [Test]
        public void GetById_BadWeather()
        {
            var res = _controller.GetById(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void Create_GoodWeather()
        {
            var companyToCreate = new CompanyRequest()
                {Name = "CrazyCells", Description = "The most crazy cells you can find out there"};
            var res = _controller.Create(companyToCreate) as OkNegotiatedContentResult<bool>;

            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, _context.Companies.ToList()[0].Id);
            Assert.AreEqual("CrazyCells", _context.Companies.ToList()[0].Name);
            Assert.AreEqual("The most crazy cells you can find out there", _context.Companies.ToList()[0].Description);
        }

        [Test]
        public void Model_Validation()
        {
            var companyRequest = new CompanyRequest()
                {Description = "A company without a name shouldn't pass"};
            var context = new ValidationContext(companyRequest, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(companyRequest, context, results, true);
            Assert.IsFalse(isModelStateValid);
        }

        [Test]
        public void Create_BadWeather()
        {
            // #1 - Try creating a company with empty/null body
            var res = _controller.Create(null) as BadRequestErrorMessageResult;

            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try creating two companies with the same name
            var companyToCreate = new CompanyRequest()
                {Name = "CrazyCells", Description = "The most crazy cells you can find out there"};
            _controller.Create(companyToCreate);

            companyToCreate = new CompanyRequest() {Name = "CrazyCells", Description = "Different description"};
            res = _controller.Create(companyToCreate) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A company with that name already exists!", res.Message);

            // #3 - Try creating a company without providing a company name
            _controller.ModelState.AddModelError("error", "A company name must be provided!");
            companyToCreate = new CompanyRequest()
                {Description = "The most crazy cells you can find out there"};
            res = _controller.Create(companyToCreate) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A company name must be provided!", res.Message);
        }

        [Test]
        public void Update_GoodWeather()
        {
            _context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            Assert.AreEqual(1, _context.Companies.ToList()[0].Id);
            Assert.AreEqual("MySuccessfulCompany", _context.Companies.ToList()[0].Name);
            Assert.AreEqual("Best company ever", _context.Companies.ToList()[0].Description);

            var updatedCompany = new CompanyRequest()
                {Description = "Some new crazy description"};

            var res = _controller.Update(1, updatedCompany) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, _context.Companies.ToList()[0].Id);
            Assert.AreEqual("MySuccessfulCompany", _context.Companies.ToList()[0].Name);
            Assert.AreEqual("Some new crazy description", _context.Companies.ToList()[0].Description);

            updatedCompany = new CompanyRequest()
                {Name = "Different Name"};

            res = _controller.Update(1, updatedCompany) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, _context.Companies.ToList()[0].Id);
            Assert.AreEqual("Different Name", _context.Companies.ToList()[0].Name);
            Assert.AreEqual("Some new crazy description", _context.Companies.ToList()[0].Description);

            updatedCompany = new CompanyRequest()
                {Name = "Another very different name", Description = "Very different super description"};

            res = _controller.Update(1, updatedCompany) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, _context.Companies.ToList()[0].Id);
            Assert.AreEqual("Another very different name", _context.Companies.ToList()[0].Name);
            Assert.AreEqual("Very different super description", _context.Companies.ToList()[0].Description);
        }

        [Test]
        public void Update_BadWeather()
        {
            // #1 - Try updating a company with empty/null body
            var res = _controller.Update(1, null) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            // #2 - Try updating a non-existing company
            var updatedCompany = new CompanyRequest()
                {Name = "MySuccessfulCompany", Description = "Some new crazy description"};
            var res1 = _controller.Update(1, updatedCompany);
            Assert.IsNotNull(res1);
            Assert.IsInstanceOf<NotFoundResult>(res1);

            // #3 - Try updating a company without providing a company name
            _controller.ModelState.AddModelError("error", "A company name must be provided!");
            updatedCompany = new CompanyRequest()
                {Description = "The most crazy cells you can find out there"};
            res = _controller.Update(1, updatedCompany) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A company name must be provided!", res.Message);
        }

        [Test]
        public void DeleteCompany_BadWeather()
        {
            var res = _controller.Delete(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }
    }
}