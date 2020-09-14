using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using API.Controllers.Company;
using Domain.Entities;
using Domain.Resources;
using NUnit.Framework;

namespace API.Tests.Company
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var context = new TestContext();
            context.Companies.Add(new CompanyEntity(){Name = "Kolo", Description = "Loco"});
            var controller = new CompanyController(context);
            var res = controller.GetAll() as OkNegotiatedContentResult<List<CompanyResource>>;

            Assert.IsNotNull(res);
            Assert.AreEqual(1, res.Content.Count);

            // Assert.True(true);
            // var testUsers = GetTestProducts();
            // var controller = new SimpleProductController(testProducts);
            //
            // var result = controller.GetAllProducts() as List<Product>;
            // Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}