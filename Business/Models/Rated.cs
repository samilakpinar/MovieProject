using Newtonsoft.Json;

namespace Business.Models
{
    public class Rated
    {
        [JsonProperty("value")]
        public float Value { get; set; }
    }
}
