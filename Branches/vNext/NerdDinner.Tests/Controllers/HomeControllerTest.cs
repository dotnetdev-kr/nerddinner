using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NerdDinner.Controllers;
using NerdDinner.Tests.Mocks;
using NUnit.Framework;

namespace NerdDinner.Tests.Controllers {
    [TestFixture]
    public class HomeControllerTest {
        [Test]
        public void Index() {
			// Arrange
			HttpContextBase httpContext = MvcMockHelpers.FakeHttpContext();
			HomeController controller = new HomeController();
			RequestContext requestContext = new RequestContext(httpContext, new RouteData());

			controller.ControllerContext = new ControllerContext(requestContext, controller);
			controller.Url = new UrlHelper(requestContext);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
        }

        [Test]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
