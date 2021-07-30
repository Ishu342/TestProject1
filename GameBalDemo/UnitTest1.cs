using FakeItEasy;
using NUnit.Framework;

namespace GameBalDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var datastore = A.Fake<HomeController>();
            var controller = new HomeController();
            //Act
            //Assert
            Assert.Pass();
        }
    }

    internal class HomeController
    {
        public HomeController()
        {
        }
    }
}