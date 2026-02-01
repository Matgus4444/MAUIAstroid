using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIMobile.Models
{
    public class NeoFeedResponse
    {
        [JsonPropertyName("near_earth_objects")]
        public Dictionary<string, List<Astroid>> NearEarthObjects { get; set; }
    }
}
