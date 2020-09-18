using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Xml.Serialization;
using API.Controllers.Company;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Company
{
    [TestFixture]
    public class CompanyTest
    {
        [Test]
        public void GetAllCompanies_GoodWeather()
        {
            var context = new TestContext();
            context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            context.Companies.Add(new CompanyEntity()
                {Name = "MyPoorCompany", Description = "Worst company ever"});
            var controller = new CompanyController(context);
            var res = controller.GetAll() as OkNegotiatedContentResult<List<CompanyResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(2, res.Content.Count);
            Assert.AreEqual("MySuccessfulCompany", res.Content[0].Name);
            Assert.AreEqual("MyPoorCompany", res.Content[1].Name);
        }

        [Test]
        public void GetAllCompanies_BadWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);
            var res = controller.GetAll() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(res);
            Assert.AreEqual("There are no companies found", res.Content);
        }

        [Test]
        public void GetCompanyById_GoodWeather()
        {
            var context = new TestContext();
            context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            var controller = new CompanyController(context);
            var res = controller.GetById(1) as OkNegotiatedContentResult<CompanyResource>;

            Assert.IsNotNull(res);
            Assert.AreEqual("MySuccessfulCompany", res.Content.Name);
        }

        [Test]
        public void GetCompanyById_BadWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);
            var res = controller.GetById(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public void CreateCompany_GoodWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);

            var companyToCreate = new CompanyRequest()
                {Name = "CrazyCells", Description = "The most crazy cells you can find out there"};
            var res = controller.Create(companyToCreate) as OkNegotiatedContentResult<bool>;

            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, context.Companies.ToList()[0].Id);
            Assert.AreEqual("CrazyCells", context.Companies.ToList()[0].Name);
            Assert.AreEqual("The most crazy cells you can find out there", context.Companies.ToList()[0].Description);
        }

        [Test]
        public void CreateCompany_BadWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);

            var res = controller.Create(null) as BadRequestErrorMessageResult;

            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);

            var companyToCreate = new CompanyRequest()
                {Name = "CrazyCells"};

            res = controller.Create(companyToCreate) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("You cannot create a company without specifying the description", res.Message);

            companyToCreate = new CompanyRequest()
                {Description = "Some crazy description"};
            res = controller.Create(companyToCreate) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("You cannot create a company without specifying the name", res.Message);


            companyToCreate = new CompanyRequest()
                {Name = "CrazyCells", Description = "The most crazy cells you can find out there"};
            controller.Create(companyToCreate);

            companyToCreate = new CompanyRequest() {Name = "CrazyCells", Description = "Different description"};
            res = controller.Create(companyToCreate) as BadRequestErrorMessageResult;
            Assert.NotNull(res);
            Assert.AreEqual("A company with that name already exists!", res.Message);
        }

        [Test]
        public void UpdateCompany_GoodWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);
            context.Companies.Add(new CompanyEntity()
                {Name = "MySuccessfulCompany", Description = "Best company ever"});
            Assert.AreEqual(1, context.Companies.ToList()[0].Id);
            Assert.AreEqual("MySuccessfulCompany", context.Companies.ToList()[0].Name);
            Assert.AreEqual("Best company ever", context.Companies.ToList()[0].Description);

            var updatedCompany = new CompanyRequest()
                {Description = "Some new crazy description"};

            var res = controller.Update(1, updatedCompany) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, context.Companies.ToList()[0].Id);
            Assert.AreEqual("MySuccessfulCompany", context.Companies.ToList()[0].Name);
            Assert.AreEqual("Some new crazy description", context.Companies.ToList()[0].Description);

            updatedCompany = new CompanyRequest()
                {Name = "Different Name"};

            res = controller.Update(1, updatedCompany) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, context.Companies.ToList()[0].Id);
            Assert.AreEqual("Different Name", context.Companies.ToList()[0].Name);
            Assert.AreEqual("Some new crazy description", context.Companies.ToList()[0].Description);
            
            updatedCompany = new CompanyRequest()
                {Name = "Another very different name", Description = "Very different super description"};

            res = controller.Update(1, updatedCompany) as OkNegotiatedContentResult<bool>;
            Assert.NotNull(res);
            Assert.IsTrue(res.Content);
            Assert.AreEqual(1, context.Companies.ToList()[0].Id);
            Assert.AreEqual("Another very different name", context.Companies.ToList()[0].Name);
            Assert.AreEqual("Very different super description", context.Companies.ToList()[0].Description);

        }

        [Test]
        public void UpdateCompany_BadWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);
            var res = controller.Update(1, null) as BadRequestErrorMessageResult;

            Assert.NotNull(res);
            Assert.AreEqual("Empty request body!", res.Message);
            
            var updatedCompany = new CompanyRequest()
                {Name = "MySuccessfulCompany", Description = "Some new crazy description"};
            var res1 = controller.Update(1, updatedCompany);
            Assert.IsNotNull(res1);
            Assert.IsInstanceOf<NotFoundResult>(res1);
        }

        [Test]
        public void DeleteCompany_BadWeather()
        {
            var context = new TestContext();
            var controller = new CompanyController(context);
            var res = controller.Delete(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }
    }
}