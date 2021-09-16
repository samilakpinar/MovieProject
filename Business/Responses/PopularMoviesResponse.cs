using Business.Models;
using System.Collections.Generic;

namespace Business.Responses
{
    public class PopularMoviesResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}
