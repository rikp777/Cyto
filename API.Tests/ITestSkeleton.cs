using NUnit.Framework;

namespace API.Tests
{
    public interface ITestSkeleton
    {
        [SetUp]
        void Init();
        
        [Test]
        void GetAll_GoodWeather();

        [Test]
        void GetAll_BadWeather();

        [Test]
        void GetById_GoodWeather();

        [Test]
        void GetById_BadWeather();

        [Test]
        void Model_Validation();

        [Test]
        void Create_GoodWeather();

        [Test]
        void Create_BadWeather();

        [Test]
        void Update_GoodWeather();

        [Test]
        void Update_BadWeather();
    }
}