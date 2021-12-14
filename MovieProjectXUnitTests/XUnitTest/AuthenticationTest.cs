using Business.Abstract;
using Business.Concrete;
using Business.Models;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationTest
    {
        private static Mock<IConfiguration> config = new Mock<IConfiguration>();
        private static Mock<IUserService> userService = new Mock<IUserService>();
        private static AuthenticationManager authenticationManager = new AuthenticationManager(config.Object, userService.Object);
       

        [Fact]
        public void Register_Test()
        {
            // Arrange : Create object for test function.
            Users users = new Users();
            users.Name = "Ali";
            users.Surname = "Ahmet";
            users.Email = "samil@gmail.com";
            users.Password = "";
            users.Permisson = 2;
           
            // Act : Trigger function.
            var result = authenticationManager.Register(users);

            // Assert : Check the result.
            Assert.False(result); // if result false; return true. Pass the test.

        }

        [Fact]
        public void Login_Test() //Autenticate
        {

            //Arrange
            User user = new User();
            user.Name = "Ali";
            user.Surname = "Ahemt";
            user.Email = "samil@gmail.com";
            user.Password = "";
            user.Permisson = 2;
            user.SessionId = "ssssss";
            user.Token = "ssss";


            //Act
            var result = authenticationManager.Authenticate(user);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateToken_Test()
        {
            //Arrange

            //Act
            var result = authenticationManager.CreateToken();

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void CreateSession_Test()
        {
            //Arrange
            CreateSession tokenValue = new CreateSession();
            tokenValue.request_token = ""; //if empty, return null.

            //Act
            var result = authenticationManager.CreateSession(tokenValue).Result;

            //Assert
            Assert.Null(result);

        }

    }
}
