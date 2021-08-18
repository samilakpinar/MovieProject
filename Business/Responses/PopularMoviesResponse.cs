using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses
{
    public class PopularMoviesResponse
    {
        public int Page { get; set; }

        public List<Movie> Results { get; set; }
    }
}
