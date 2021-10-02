using Newtonsoft.Json;
using Shelf.Enum;
using System.Collections.Generic;

namespace Shelf.Entity
{
    public class LocalMedia
    {
        [JsonProperty("paths")]
        public List<LocalMediaPaths> paths { get; set; }
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
