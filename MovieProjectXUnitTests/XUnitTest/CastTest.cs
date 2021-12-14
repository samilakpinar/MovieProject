using Business.Concrete;
using Business.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    public class CastTest
    {
        [Fact]
        public void Get_Populer_Cast_Test()
        {
            //Arrange
            int movieId = -1;

            //Act
            var appSetting = new Mock<IOptions<AppSettings>>(); //added Appsettings class for dependency
            var castManager = new CastManager(appSetting.Object);
            var result = castManager.GetPopulerCast(movieId).Result;

            //Assert
            Assert.Null(result);
        }
    }
}
