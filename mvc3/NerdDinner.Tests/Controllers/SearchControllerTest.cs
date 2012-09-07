using System.Collections.Generic;
using NerdDinner.Controllers;
using System.Web.Mvc;
using NerdDinner.Tests.Fakes;
using NUnit.Framework;

namespace NerdDinner.Tests.Controllers
{
    [TestFixture]
    public class SearchControllerTest {

        SearchController CreateSearchController() {
            var testData = FakeDinnerData.CreateTestDinners();
            var repository = new FakeDinnerRepository(testData);

            return new SearchController(repository);
        }

        [Test]
        public void SearchByLocationAction_Should_Return_Json()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = controller.SearchByLocation(99, -99);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void SearchByLocationAction_Should_Return_JsonDinners()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.SearchByLocation(99, -99);

            // Assert
            Assert.IsInstanceOf<List<JsonDinner>>(result.Data);
            var dinners = (List<JsonDinner>)result.Data;
            Assert.AreEqual(101, dinners.Count);
        }

        [Test]
        public void GetMostPopularDinnersAction_WithLimit_Returns_Expected_Dinners()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.GetMostPopularDinners(5);

            // Assert
            Assert.IsInstanceOf<List<JsonDinner>>(result.Data);
            var dinners = (List<JsonDinner>)result.Data;
            Assert.AreEqual(5, dinners.Count);
        }

        [Test]
        public void GetMostPopularDinnersAction_WithNoLimit_Returns_Expected_Dinners()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.GetMostPopularDinners(null);

            // Assert
            Assert.IsInstanceOf<List<JsonDinner>>(result.Data);
            var dinners = (List<JsonDinner>)result.Data;
            Assert.AreEqual(40, dinners.Count);
        }


    }
}
