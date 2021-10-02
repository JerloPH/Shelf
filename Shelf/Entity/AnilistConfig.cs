using Newtonsoft.Json;

namespace Shelf.Json
{
    public class AnilistConfig
    {
        [JsonProperty("clientId")]
        public string clientId { get; set; } = "";
        [JsonProperty("clientSecret")]
        public string clientSecret { get; set; } = "";
    }
}
