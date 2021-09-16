using MovieProjectXUnitTests.ServicesTest;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    public class CastControllerTest
    {

        [Fact]
        public void Get_Populer_Cast_Test()
        {
            //Arrange
            int movieId = 1;
            var castManager = new CastManagerTest();

            //Act
            var result = castManager.GetPopulerCast(movieId);

            //Assert
            Assert.Null(result.Result);
        }
    }
}
