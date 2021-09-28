using Newtonsoft.Json;
using Shelf.Enum;
using System.Collections.Generic;

namespace Shelf.Entity
{
    public class LocalMedia
    {
        [JsonProperty("anime_paths")]
        public List<LocalMediaPaths> anime_paths { get; set; }

        [JsonProperty("manga_paths")]
        public List<LocalMediaPaths> manga_paths { get; set; }
    }

    public class LocalMediaPaths
    {
        [JsonProperty("folder")]
        public string folder { get; set; }

        [JsonProperty("isSeparateSources")]
        public bool isSeparateSources { get; set; }

        [JsonProperty("MediaType")]
        public MediaAniManga mediaType { get; set; }
    }
}
