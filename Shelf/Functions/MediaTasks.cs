using Newtonsoft.Json;
using ProtoBuf;
using Shelf.Entity;
using Shelf.Enum;
using Shelf.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.Functions
{
    public static class MediaTasks
    {
        public async static Task<List<Entry>> GetMediaList(string file)
        {
            var result = new List<Entry>(); // resulting list
            var idList = new List<long>(); // already existing entries
            await Task.Run((Action)delegate
            {
                var mediaJson = GlobalFunc.ReadMediaJson(file);
                if (mediaJson != null)
                {
                    foreach (var list in mediaJson.Data.MediaListCollection.Lists)
                    {
                        foreach (var item in list.Entries)
                        {
                            if (!idList.Contains(item.Media.Id))
                            {
                                result.Add(item);
                                idList.Add(item.Media.Id);
                            }
                        }
                    }
                }
            });
            idList.Clear();
            return result;
        }
        public static async Task<List<Entry>> GetEntriesStripByMode(List<Entry> data, MediaEntryMode mode)
        {
            return await Task.Run(delegate
            {
                var data2 = new List<Entry>();
                foreach (var item in data)
                {
                    if (mode == MediaEntryMode.MAL)
                    {
                        // Include only MAL entries
                        if (item.Media.IdMal != null)
                        {
                            if (item.Media.IdMal > 0)
                                data2.Add(item);
                        }
                    }
                    else
                    {
                        // Skip MAL Entries
                        if (item.Media.IdMal == null)
                            data2.Add(item);
                        else
                        {
                            if (item.Media.IdMal > 0)
                                continue;
                        }
                    }
                }
                return data2;
            });
        }
        public static async Task<List<Entry>> GetAnimeList(MediaEntryMode mode)
        {
            var data = await GetMediaList(GlobalFunc.FILE_ANIME);
            if (mode == MediaEntryMode.All)
                return data;

            return await GetEntriesStripByMode(data, mode);
        }
        public static async Task<List<Entry>> GetMangaList(MediaEntryMode mode)
        {
            var data = await GetMediaList(GlobalFunc.FILE_MANGA);
            if (mode == MediaEntryMode.All)
                return data;

            return await GetEntriesStripByMode(data, mode);
        }
        public static async Task ProcessMedia(List<Entry> medialist, string media, string outputfile, string username, string outputNonMal)
        {
            string xmltoWrite = "";
            string AnilistStatus = "";
            string prepend = "";
            var count = new StatusCount();
            var nonMal = new List<Entry>();
            // Run task
            await Task.Run((Action)delegate
            {
                foreach (var item in medialist)
                {
                    if (item.Media.IdMal != null)
                    {
                        xmltoWrite = MAL.XmlMedia(media, item);
                        GlobalFunc.AppendFile(outputfile, xmltoWrite);

                        // Add count
                        AnilistStatus = item.Status;
                        count.Total += 1;
                        switch (AnilistStatus)
                        {
                            case "COMPLETED":
                                count.Complete += 1;
                                break;
                            case "PAUSED":
                                count.Onhold += 1;
                                break;
                            case "CURRENT":
                                count.Current += 1;
                                break;
                            case "DROPPED":
                                count.Drop += 1;
                                break;
                            case "PLANNING":
                                count.Plan += 1;
                                break;
                            case "REPEATING":
                                count.Current += 1;
                                break;
                        }
                    }
                    else
                    {
                        nonMal.Add(item);
                    }
                }
                // Append finalizer
                xmltoWrite = $"</my{media}list>";
                GlobalFunc.AppendFile(outputfile, xmltoWrite);
                // Prepend 'myinfo' tree
                prepend = MAL.PrependInfo(media, username, count);
                GlobalFunc.PrependFile(outputfile, prepend);
                GlobalFunc.WriteObjectToJson(outputNonMal, nonMal); // Serialize to json
            });
        }
        public static async Task<BackupTachiProto> LoadTachiyomiBackup(string file)
        {
            if (!File.Exists(file)) { Logs.Err($"Tachiyomi backup is missing => {file}"); return null; }
            try
            {
                string filetoRead = file;
                BackupTachiProto tachi = null;
                return await Task.Run(delegate
                {
                    if (file.Substring(file.Length - 2).Equals("gz"))
                    {
                        filetoRead = GlobalFunc.Decompress(file, $"{Path.Combine(GlobalFunc.DIR_TEMP, $"tachiyomiBackup_{GlobalFunc.DATE_TODAY}.proto")}");
                    }
                    using (var ms = File.OpenRead(filetoRead))
                    {
                        ms.Position = 0;
                        tachi = Serializer.Deserialize<BackupTachiProto>(ms);
                        ms.Close();
                    }
                    return tachi;
                });
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
            }
            return null;
        }
        public static async Task<List<Entry>> GenerateMissingTachiEntries(string file)
        {
            string categoryName = "Anilist";
            string outputPrefix = $"tachiyomi_{GlobalFunc.DATE_TODAY}";
            string outputTachiLib = Path.Combine(GlobalFunc.DIR_OUTPUT, $"{outputPrefix}_Library.json");
            string outputProto = Path.Combine(GlobalFunc.DIR_OUTPUT, $"{outputPrefix}_NotInTachi.proto");
            string outputJson = Path.Combine(GlobalFunc.DIR_OUTPUT, $"{outputPrefix}_NotInTachi.json");
            string outputTachiNoTracker = Path.Combine(GlobalFunc.DIR_OUTPUT, $"{outputPrefix}_NoTrackers.json");
            bool canAdd = true;
            bool tachiloaded = true; // Loaded tachiyomi backup file flag
            int categoryId = 0;
            var tachilist = new List<long>(); // list of entries in Tachi lib
            var tachilistNames = new List<string>();
            var mangalist = new List<long>(); // trimmed list of entries from Anilist, tachi entries removed
            var backupmangalist = new List<BackupManga>(); // List of BackupManga objects, entries from 'mangalist'
            var backupMangaJson = new List<BackupMangaJson>(); // Json backup list
            BackupTachiProto tachi = null;
            var listEntries = new List<Entry>();
            // Start Task
            return await Task.Run(async delegate
            {
                try
                {
                    tachi = await LoadTachiyomiBackup(file);
                    if (tachi != null)
                    {
                        GlobalFunc.WriteObjectToJson(outputTachiLib, tachi); // Serialize to json file
                        // Iterate over the items
                        foreach (var item in tachi.Mangas)
                        {
                            if (item.Tracking?.Count > 0)
                            {
                                foreach (var track in item.Tracking)
                                {
                                    if (track.TrackingUrl.Contains("anilist.co"))
                                    {
                                        if (!tachilist.Contains(track.MediaId)) // prevent duplicate entries
                                        {
                                            tachilist.Add(track.MediaId);
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (!String.IsNullOrWhiteSpace(item.Title))
                                {
                                    if (!tachilistNames.Contains(item.Title))
                                        tachilistNames.Add(item.Title);
                                }
                            }
                        }
                        // Get highest category Order
                        foreach (var cat in tachi.backupCategories)
                        {
                            if (cat.Name.Equals(categoryName))
                            {
                                categoryId = cat.Order;
                                break;
                            }
                            if (cat.Order >= categoryId)
                            {
                                categoryId = cat.Order + 1; // set "category" 'Order' for Tachi backup output
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logs.Err(ex);
                    GlobalFunc.Alert("Error loading Tachiyomi backup file!");
                    tachiloaded = false;
                    tachilist.Clear(); // Micro optimization
                    mangalist.Clear();
                    tachilistNames.Clear();
                    backupmangalist.Clear();
                    backupMangaJson.Clear();
                    return listEntries;
                }
                if (tachiloaded)
                {
                    GlobalFunc.WriteObjectToJson(outputTachiNoTracker, tachilistNames); // serialize list of tachi entries titles without trackers
                    // Fetch manga Ids from Anilist
                    var manga = await GetMangaList(MediaEntryMode.All);
                    foreach (var item in manga)
                    {
                        if (item.Media.Format.Equals("MANGA") || item.Media.Format.Equals("ONE_SHOT"))
                        {
                            canAdd = !GlobalFunc.SKIP_STATUS.Contains(item.Status);
                            if (!mangalist.Contains(item.Media.Id) && canAdd)
                            {
                                // Entry is missing from Tachi lib
                                if (!tachilist.Contains(item.Media.Id) && !tachilistNames.Contains(item.Media.Title.Romaji))
                                {
                                    mangalist.Add(item.Media.Id);
                                    // Add entry for proto
                                    var entry = new BackupManga();
                                    //var tracker = new BackupTracking();
                                    //tracker.MediaId = (int)item.Media.Id;
                                    //tracker.TrackingUrl = @"https://anilist.co/manga/" + item.Media.Id.ToString();
                                    //tracker.syncId = 0;
                                    //tracker.libraryId = 0;
                                    //maxLibId += 1; // Add 1
                                    //entry.Tracking.Add(tracker);
                                    entry.Tracking = null;
                                    entry.Title = item.Media.Title.Romaji;
                                    entry.source = 0;
                                    entry.url = item.Media.Title.Romaji;
                                    entry.Categories = new List<int>();
                                    entry.Categories.Add(categoryId);
                                    backupmangalist.Add(entry);
                                    // Add entry for json
                                    var jsonEntry = new BackupMangaJson();
                                    jsonEntry.manga = new object[] { item.Media.Title.Romaji, item.Media.Title.Romaji, 0, 0, 0 };
                                    jsonEntry.categories = new string[] { categoryName };
                                    backupMangaJson.Add(jsonEntry);

                                    // Add entry to result list
                                    var newEntry = new Entry();
                                    newEntry.Media = new Media();
                                    newEntry.Media.Title = new Title();
                                    newEntry.Media.Id = 0;
                                    newEntry.Media.Title.Romaji = entry.Title;
                                    listEntries.Add(newEntry);
                                }
                            }
                        }
                    }
                    tachilist.Clear(); // Micro optimization
                    mangalist.Clear();
                    tachilistNames.Clear();
                    // Save output files
                    if (backupmangalist?.Count > 0)
                    {
                        // Serialize new backup Tachi file, with Anilist entries not on Tachi
                        // Serialize to proto file
                        try
                        {
                            var backuptachi = new BackupTachiProto();
                            var backupcat = new BackupCategories();
                            backuptachi.Mangas = backupmangalist;
                            backuptachi.backupCategories.Clear();
                            backupcat.Name = categoryName;
                            backupcat.Order = categoryId;
                            backuptachi.backupCategories.Add(backupcat);
                            var streamdata = GlobalFunc.ProtoSerialize(backuptachi);
                            if (streamdata != null)
                            {
                                File.WriteAllBytes(outputProto, streamdata);
                                GlobalFunc.Compress(outputProto);
                            }
                        }
                        catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Cannot serialize proto backup file!"); }
                        // Serialize to json file
                        try
                        {
                            var backupTachiJson = new BackupTachiJson();
                            backupTachiJson.version = 2;
                            backupTachiJson.Mangas = backupMangaJson;
                            backupTachiJson.Categories.Add(new object[] { categoryName, categoryId });
                            if (!GlobalFunc.WriteObjectToJson(outputJson, backupTachiJson))
                                GlobalFunc.Alert("Cannot serialize json backup file!");
                        }
                        catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Cannot serialize json backup file!\nError occured."); };
                    }
                    backupmangalist.Clear();
                    backupMangaJson.Clear();
                }
                return listEntries;
            });
        }
        public static async Task<List<Entry>> GetTachiWithAnilist(List<Entry> manga, List<BackupManga> listofManga, MediaEntryMode mode)
        {
            return await Task.Run(async delegate
            {
                var entries = new List<Entry>();
                if (manga.Count < 1)
                    manga = await GetMangaList(MediaEntryMode.All);

                foreach (var item in listofManga)
                {
                    if (item.Tracking?.Count > 0)
                    {
                        foreach (var track in item.Tracking)
                        {
                            if (track.TrackingUrl.Contains("anilist", StringComparison.OrdinalIgnoreCase))
                            {
                                var mangaExisting = entries.Select(x => x).Where(x => x.Media.Id == track.MediaId);
                                if (!mangaExisting.Any())
                                {
                                    var mangaQuery = manga.Select(x => x).Where(x => x.Media.Id == track.MediaId);
                                    if (mangaQuery.Count() == 1)
                                        entries.Add(mangaQuery.First());
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (item != null)
                        {
                            try
                            {
                                var newEntry = new Entry();
                                newEntry.Media = new Media();
                                newEntry.Media.Title = new Title();
                                if (!String.IsNullOrWhiteSpace(item.Title))
                                    newEntry.Media.Title.Romaji = item.Title;

                                newEntry.Media.Id = 0;
                                entries.Add(newEntry);
                            }
                            catch (Exception ex) { Logs.Err(ex); }
                        }
                    }
                }
                if (mode == MediaEntryMode.All)
                    return entries;
                else
                    return await GetEntriesStripByMode(entries, mode);
            });
        }
        // ############################################################ End of Class
    }
}
