using Business.Models;
using Business.Responses;
using MovieProject.Controllers;
using MovieProjectXUnitTests.ServicesTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
