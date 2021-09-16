using MovieProjectXUnitTests.ServicesTest;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void Get_Sidebar_Menu_Test()
        {
            //Arrange
            string role = "0";
            var homeManager = new HomeManagerTest();

            //Act
            var result = homeManager.GetMenuList(role);

            //Assert
            Assert.Equal(null, result);
        }


    }
}
