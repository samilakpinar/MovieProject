using Business.Models;
using Entities.Concrete;
using MovieProjectXUnitTests.ServicesTest;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationControllerTest
    {

        [Fact]
        public void Register_Test()
        {

            // Arrange : Test edilecek metodun nesneleri yaratılır.
            Users users = new Users();
            users.Name = "Ali";
            users.Surname = "Ahemt";
            users.Email = "samil@gmail.com";
            users.Password = "";
            users.Permisson = 2;

            var result = new AuthenticationManagerTest();

            // Act : İlgili nesneden hangi metodu test edeceksek arrange bölümündeki nesne ile bu metot tetiklenir.      

            // Assert : Sonuca ulaşıp ulaşılmadığı kontrol edilir.
            Assert.Equal(false, result.Register(users));

        }

        [Fact]
        public void Authenticate_Test()
        {

            User user = new User();
            user.Name = "Ali";
            user.Surname = "Ahemt";
            user.Email = "samil@gmail.com";
            user.Password = "";
            user.Permisson = 2;
            user.SessionId = "ssssss";
            user.Token = "ssss";


            var result = new AuthenticationManagerTest();

            Assert.Equal(null, result.Authenticate(user));
        }


        [Fact]
        public void CreateToken_Test()
        {

            var authManager = new AuthenticationManagerTest();

            var result = authManager.CreateToken();

            Assert.NotNull(result);

        }

        [Fact]
        public void CreateSession_Test()
        {
            CreateSession tokenValue = new CreateSession();
            tokenValue.request_token = ""; //boş ise null döner.

            var authManager = new AuthenticationManagerTest();

            var result = authManager.CreateSession(tokenValue);

            Assert.Null(result.Result);

        }

        [Fact]
        public void CheckEmail_Test()
        {
            ValidationEmail validate = new ValidationEmail();
            validate.Email = "samilakpinar6@gmail.com";
            validate.Token = ""; //boş ise null döner.

            var result = new AuthenticationManagerTest();

            Assert.Equal(false, result.ValidationEmail(validate));

        }

    }
}
