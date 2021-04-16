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
        [JsonProperty("User")]
        public User user { get; set; }
    }
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
    }
}
