using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
