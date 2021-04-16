using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shelf.Json
{
    public class AnilistData
    {
        [JsonProperty("data")]
        public Data data { get; set; }
    }
    public class Data
    {
        [JsonProperty("Viewer")]
        public Viewer Viewer { get; set; }
    }
    public class Viewer
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
    }
}
