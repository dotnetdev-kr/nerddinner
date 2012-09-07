using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;
using System.IO;

namespace NerdDinner.Tests.Helpers
{
    public class MockHelpers<T>
    {
        public static HtmlHelper<T> CreateHtmlHelper()
        {
            Mock<ViewContext> mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object),
                new Mock<IView>().Object,
                new ViewDataDictionary(),
                new TempDataDictionary(),
                new Mock<TextWriter>().Object);
            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData)
            .Returns(new ViewDataDictionary());
            return new HtmlHelper<T>(mockViewContext.Object,
                                     mockViewDataContainer.Object);

        }
    }
}