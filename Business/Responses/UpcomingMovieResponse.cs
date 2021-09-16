using Business.Models;
using System.Collections.Generic;

namespace Business.Responses
{
    public class UpcomingMovieResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}
