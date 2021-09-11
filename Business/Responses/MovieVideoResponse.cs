using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses
{
    public class MovieVideoResponse
    {
        public int Id { get; set; }
        public List<MovieVideo> Results { get; set; }
    }
}
