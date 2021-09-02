using Business.Models;
using MovieProjectXUnitTests.ServicesTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    public class MoviesControllerTest
    {
        [Fact]
        public void Get_Populer_Movie_Test()
        {
            //Arrange
            int page = 0;
            var movieManager = new MoviesManagerTest();

            //Act
            var result = movieManager.GetAllPopulerMovies(page);

            //Assert
            Assert.Null(result.Result);
            
        }

        [Fact]
        public void Get_Movie_By_Id()
        {
            //Arrange
            string movie_id = "23";
            var movieManager = new MoviesManagerTest();

            //Act
            var result = movieManager.GetMovieById(movie_id);

            //Assert
            Assert.Null(result.Result);
        }

        [Fact]
        public void Get_Rate_Movie_Test()
        {

            //Arrange
            int movieId = 2;
            string sessionId = "";
            string guestId = "";
            var movieManager = new MoviesManagerTest();

            //Act
            var result = movieManager.GetRateMovie(movieId, sessionId, guestId);

            //Assert
            Assert.Null(result.Result);

        }


        [Fact]
        public void Rate_Movie_Test()
        {
            
            //Arrange
            RateMovie movie = new RateMovie();
            movie.MovieId = 550;
            movie.GuestId = "";
            movie.SessionId = "";
            movie.value = -4; //Get null

            var movieManager = new MoviesManagerTest();

            //Act
            var result = movieManager.RateMovie(movie);

            //Assert
            Assert.Null(result.Result);
        }

    }
}
