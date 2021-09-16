using Business.Models;
using System.Collections.Generic;

namespace Business.Responses
{
    public class MovieVideoResponse
    {
        public int Id { get; set; }
        public List<MovieVideo> Results { get; set; }
    }
}
