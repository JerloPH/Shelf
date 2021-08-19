using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Shelf.Entity
{
    [ProtoContract]
    public class BackupTachi
    {
        [ProtoMember(1)]
        public List<BackupManga> Main { get; set; } = new List<BackupManga>();
    }

    [ProtoContract]
    public class BackupManga
    {
        [ProtoMember(3)]
        public string Title { get; set; } = "";
        [ProtoMember(18)]
        public List<BackupTracking> Tracking { get; set; } = new List<BackupTracking>();
    }
    [ProtoContract]
    public class BackupTracking
    {
        [ProtoMember(1)]
        public int SyncId { get; set; } = 0;
        [ProtoMember(2)]
        public long LibId { get; set; } = 0;
        [ProtoMember(3)]
        public int MediaId { get; set; } = 0;
        [ProtoMember(4)]
        public string TrackingUrl { get; set; } = "";
    }
}
