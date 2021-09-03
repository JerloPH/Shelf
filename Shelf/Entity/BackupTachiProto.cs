using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Shelf.Entity
{
    [ProtoContract(Name = "Backup")]
    public class BackupTachiProto
    {
        [ProtoMember(1, Name = "backupManga")]
        public List<BackupManga> Mangas { get; set; } = new List<BackupManga>();

        [ProtoMember(2, Name = "backupCategories")]
        public List<BackupCategories> backupCategories { get; set; } = new List<BackupCategories>();
    }

    [ProtoContract(Name = "BackupManga")]
    public class BackupManga
    {
        [ProtoMember(1, Name = "source"), DefaultValue(-1)]
        public long source { get; set; } = 0;

        [ProtoMember(2, Name = "url")]
        public string url { get; set; } = "";

        [ProtoMember(3, Name = "title")]
        public string Title { get; set; } = "";

        [ProtoMember(17, Name = "categories")]
        public List<int> Categories { get; set; } = new List<int>();

        [ProtoMember(18, Name = "tracking")]
        public List<BackupTracking> Tracking { get; set; } = new List<BackupTracking>();
    }

    [ProtoContract(Name = "BackupTracking")]
    public class BackupTracking
    {
        [ProtoMember(1, Name = "syncId"), DefaultValue(-1)]
        public int syncId { get; set; } = 0;

        [ProtoMember(2, Name = "libraryId"), DefaultValue(-1)]
        public long libraryId { get; set; } = 0;

        [ProtoMember(3, Name = "mediaId"), DefaultValue(-1)]
        public int MediaId { get; set; } = 0;

        [ProtoMember(4, Name = "trackingUrl")]
        public string TrackingUrl { get; set; } = "";
    }

    [ProtoContract(Name = "BackupCategory")]
    public class BackupCategories
    {
        [ProtoMember(1, Name = "name")]
        public string Name { get; set; } = "";

        [ProtoMember(2, Name = "order"), DefaultValue(-1)]
        public int Order { get; set; } = 0;
    }
}
