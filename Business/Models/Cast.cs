using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Cast
    {
        public bool Adult { get; set; }

        public int Gender { get; set; }
        
        public int id { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }
        public string Name { get; set; }
        public float Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string profilePath { get; set; }

        [JsonProperty("cast_id")]
        public int castId { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
    }
}
