using Newtonsoft.Json;
using Shelf.CustomEnums;
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
    #nullable enable
    public class LocalMediaDetails
    {
        [JsonProperty("title")]
        public string? title { get; set; }

        [JsonProperty("titleRomaji")]
        public string? titleRomaji { get; set; }

        [JsonProperty("author")]
        public string? author { get; set; }

        [JsonProperty("artist")]
        public string? artist { get; set; }

        [JsonProperty("description")]
        public string? description { get; set; }

        [JsonProperty("genre")]
        public string[]? genre { get; set; }

        [JsonProperty("status")]
        public string? status { get; set; }

        public LocalMediaDetails()
        {
            title = "";
            titleRomaji = "";
            author = "";
            artist = "";
            description = "";
            genre = new string[] { };
            status = "0";
        }
    }
}
