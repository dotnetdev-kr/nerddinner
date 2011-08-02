using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdDinner.Controllers;

namespace NerdDinner.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
			// Arrange
			HttpContextBase httpContext = FakeHttpContext();
			HomeController controller = new HomeController();
			RequestContext requestContext = new RequestContext(httpContext, new RouteData());

			controller.ControllerContext = new ControllerContext(requestContext, controller);
			controller.Url = new UrlHelper(requestContext);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


		/// <summary>
		/// Adapted from http://www.hanselman.com/blog/ASPNETMVCSessionAtMix08TDDAndMvcMockHelpers.aspx
		/// </summary>
		/// <returns></returns>
		private HttpContextBase FakeHttpContext() {
			var httpContext = new Mock<HttpContextBase>();
			var request = new Mock<HttpRequestBase>();
			var response = new Mock<HttpResponseBase>();
			var session = new Mock<HttpSessionStateBase>();
			var server = new Mock<HttpServerUtilityBase>();

			httpContext.SetupGet(ctx => ctx.Request).Returns(request.Object);
			httpContext.SetupGet(ctx => ctx.Response).Returns(response.Object);
			httpContext.SetupGet(ctx => ctx.Session).Returns(session.Object);
			httpContext.SetupGet(ctx => ctx.Server).Returns(server.Object);

			return httpContext.Object;
		}
    }
}
