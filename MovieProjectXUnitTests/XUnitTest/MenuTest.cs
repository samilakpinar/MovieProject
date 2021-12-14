using Business.Concrete;
using DataAccess.Abstract;
using Moq;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    public class MenuTest
    {
        [Fact]
        public void Get_Sidebar_Menu_Test()
        {
            //Arrange
            int role = 0; //The role takes the value 1,2,3

            //Act
            var menuRepository = new Mock<IMenuRepository>();
            var menuManager = new MenuManager(menuRepository.Object);
            var result = menuManager.GetMenuByPermissonId(role);

            //Assert
            Assert.Null(result); //if result null, return true
        }

        [Fact]
        public void Get_Sidebar_Menu_Test2()
        {
            //Arrange
            int role = 4; 

            //Act
            var menuRepository = new Mock<IMenuRepository>();
            var menuManager = new MenuManager(menuRepository.Object);
            var result = menuManager.GetMenuByPermissonId(role);

            //Assert
            Assert.Null(result);
        }
    }
}
