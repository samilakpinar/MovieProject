using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Rated
    {
        [JsonProperty("value")]
        public float Value { get; set; }
    }
}
