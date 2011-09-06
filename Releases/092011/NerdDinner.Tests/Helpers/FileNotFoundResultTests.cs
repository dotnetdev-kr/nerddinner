using System.Web;
using System.Web.Mvc;
using NerdDinner.Helpers;
using NUnit.Framework;

namespace NerdDinner.Tests.Helpers
{
    [TestFixture]
    public class FileNotFoundResultTests
    {
        [Test]
        public void ExecuteResult_Throws_HttpException_With_404_Status() {
            // Arrange
            ControllerContext context = new ControllerContext(); 
            FileNotFoundResult result = new FileNotFoundResult();

            // Act
            var thrownException = UnitTestHelper.AssertThrows<HttpException>(() => result.ExecuteResult(context));
            
            // Assert
            Assert.AreEqual(404, thrownException.GetHttpCode());
        }
    }
}
