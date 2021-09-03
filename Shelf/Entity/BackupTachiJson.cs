using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shelf.Entity
{
    public class BackupTachiJson
    {
        [JsonProperty("version")]
        public int version { get; set; } = 0;

        [JsonProperty("mangas")]
        public List<BackupMangaJson> Mangas { get; set; } = new List<BackupMangaJson>();

        [JsonProperty("categories")]
        public List<object> Categories { get; set; } = new List<object>();
    }
    public class BackupMangaJson
    {
        [JsonProperty("manga")]
        public object[] manga { get; set; } = null;

        [JsonProperty("categories")]
        public string[] categories { get; set; } = new string[] { "" };
    }
}
