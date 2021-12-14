using Business.Concrete;
using Business.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MovieProjectXUnitTests.ControllersTest
{
    public class MovieTest
    {
        private static Mock<IOptions<AppSettings>> appSettings = new Mock<IOptions<AppSettings>>();
        private static MovieManager movieManager = new MovieManager(appSettings.Object);

        [Fact]
        public void Get_Populer_Movie_Test()
        {
            //Arrange
            int page = -1;

            //Act
            var result = movieManager.GetAllPopulerMovies(page).Result;

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void Get_Movie_By_Id_Test()
        {
            //Arrange
            int movie_id = -1;

            //Act
            var result = movieManager.GetMovieById(movie_id).Result;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_Rate_Movie_Test()
        {

            //Arrange
            int movieId = -2;
            string sessionId = "";
            string guestId = "";

            //Act
            var result = movieManager.GetRateMovie(movieId, sessionId, guestId).Result;

            //Assert
            Assert.Null(result);

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

            //Act
            var result = movieManager.RateMovie(movie).Result;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_Upcoming_Movie_Test()
        {

            //Arrange
            int page = 0;

            //Act
            var result = movieManager.GetUpcomingMovie(page).Result;

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void Get_Movie_Video_By_Id_Test()
        {
            //Arrange
            int movieId = -1;

            //Act
            var result = movieManager.GetMovieVideoById(movieId).Result;

            //Assert
            Assert.Null(result);
        }

    }
}

