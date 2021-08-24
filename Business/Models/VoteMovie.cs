using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class VoteMovie
    {
        public int Id { get; set; }
        public bool Favorite { get; set; }

        [JsonProperty("rated")]
        public Rated? Rated { get; set; }
        public bool WatchList { get; set; }

    }
}
