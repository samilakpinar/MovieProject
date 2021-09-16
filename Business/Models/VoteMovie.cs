using Newtonsoft.Json;

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
