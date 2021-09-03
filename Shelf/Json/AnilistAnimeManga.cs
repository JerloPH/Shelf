using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shelf.Json
{
    public class AnilistAnimeManga
    {
        [JsonProperty("data")]
        public MediaData Data { get; set; }
    }

    public class MediaData
    {
        [JsonProperty("MediaListCollection")]
        public MediaListCollection MediaListCollection { get; set; }
    }

    public class MediaListCollection
    {
        [JsonProperty("lists")]
        public List<MediaList> Lists { get; set; }
    }

    public class MediaList
    {
        [JsonProperty("name")]
        public string Name { get; set; } // Name of List entry belongs to.

        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; } // List of Entries.
    }

    public partial class Entry
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("completedAt")]
        public EntryDate CompletedAt { get; set; }

        [JsonProperty("startedAt")]
        public EntryDate StartedAt { get; set; }

        [JsonProperty("progress")]
        public long? Progress { get; set; }

        [JsonProperty("progressVolumes")]
        public long? ProgressVolumes { get; set; }

        [JsonProperty("score")]
        public long? Score { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; } = "";

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }
    }

    public partial class EntryDate
    {
        [JsonProperty("year")]
        public long? Year { get; set; }

        [JsonProperty("month")]
        public long? Month { get; set; }

        [JsonProperty("day")]
        public long? Day { get; set; }
    }

    public partial class Media
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("idMal")]
        public long? IdMal { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("seasonYear")]
        public long? SeasonYear { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("episodes")]
        public long? Episodes { get; set; }

        [JsonProperty("chapters")]
        public int? Chapters { get; set; }

        [JsonProperty("volumes")]
        public int? Volumes { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("coverImage")]
        public CoverImage CoverImage { get; set; }

        [JsonProperty("synonyms")]
        public List<string> Synonyms { get; set; } = new List<string>();
    }

    public partial class CoverImage
    {
        [JsonProperty("medium")]
        public string Medium { get; set; }
    }

    public partial class Title
    {
        [JsonProperty("english")]
        public string English { get; set; } = "";

        [JsonProperty("romaji")]
        public string Romaji { get; set; } = "";
    }
}
