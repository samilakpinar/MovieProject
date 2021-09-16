using Newtonsoft.Json;

namespace Business.Models
{
    public class MovieVideo
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Site { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public bool Official { get; set; }

        [JsonProperty("publish_at")]
        public string PublishAt { get; set; }
        public string Id { get; set; }
    }
}
