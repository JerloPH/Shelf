using Newtonsoft.Json;
using ProtoBuf;
using Shelf.Entity;
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
        public static async Task<List<Entry>> GetAnimeList()
        {
            return await GetMediaList(GlobalFunc.FILE_ANIME);
        }
        public static async Task<List<Entry>> GetMangaList()
        {
            return await GetMediaList(GlobalFunc.FILE_MANGA);
        }
        public static async Task ProcessMedia(List<Entry> medialist, string media, string outputfile, string username, string outputNonMal)
        {
            string xmltoWrite = "";
            string AnilistStatus = "";
            string prepend = "";
            var count = new StatusCount();
            var nonMal = new List<Entry>();

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
                try
                {
                    string json = JsonConvert.SerializeObject(nonMal);
                    GlobalFunc.WriteFile(outputNonMal, json);
                }
                catch (Exception ex) { Logs.Err(ex); };
            });
        }
        public static async Task GenerateMissingTachiEntries(string file)
        {
            int categoryId = 0;
            string categoryName = "Anilist";
            string outputProto = file.Substring(0, file.Length - 6) + "_NotInTachi.proto";
            string outputJson = file.Substring(0, file.Length - 6) + "_NotInTachi.json";
            bool canAdd = true;
            var tachilist = new List<long>(); // list of entries in Tachi lib
            var mangalist = new List<long>(); // trimmed list of entries from Anilist, tachi entries removed
            var backupmangalist = new List<BackupManga>(); // List of BackupManga objects, entries from 'mangalist'
            var backupMangaJson = new List<BackupMangaJson>(); // Json backup list
            BackupTachiProto tachi = null;
            long maxLibId = 0;
            await Task.Run((Action)async delegate
            {
                try
                {
                    string value = GlobalFunc.ReadFromFile(file);
                    using (var ms = File.OpenRead(file))
                    {
                        ms.Position = 0;
                        tachi = Serializer.Deserialize<BackupTachiProto>(ms);
                        ms.Close();
                    }
                    if (tachi != null)
                    {
                        foreach (var item in tachi.Mangas)
                        {
                            foreach (var track in item.Tracking)
                            {
                                if (track.TrackingUrl.Contains("anilist.co"))
                                {
                                    if (!tachilist.Contains(track.MediaId)) // prevent duplicate entries
                                    {
                                        tachilist.Add(track.MediaId);
                                    }
                                }
                                // Fetch highest library Id
                                if (track.libraryId >= maxLibId)
                                    maxLibId = track.libraryId + 1;
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
                catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Error loading Tachiyomi backup file!"); }
                // Fetch manga Ids from Anilist
                var manga = await GetMangaList();
                foreach (var item in manga)
                {
                    if (item.Media.Format.Equals("MANGA") || item.Media.Format.Equals("ONE_SHOT"))
                    {
                        canAdd = !item.Status.Equals("COMPLETED") && !item.Status.Equals("DROPPED");
                        if (!mangalist.Contains(item.Media.Id) && canAdd)
                        {
                            if (!tachilist.Contains(item.Media.Id)) // Entry is missing from Tachi lib
                            {
                                mangalist.Add(item.Media.Id);
                                // Add entry for proto
                                var entry = new BackupManga();
                                //var tracker = new BackupTracking();
                                //tracker.MediaId = (int)item.Media.Id;
                                //tracker.TrackingUrl = @"https://anilist.co/manga/" + item.Media.Id.ToString();
                                //tracker.syncId = 0;
                                //tracker.libraryId = maxLibId;
                                //maxLibId += 1; // Add 1
                                //entry.Tracking.Add(tracker);
                                entry.Tracking = null;
                                entry.Title = item.Media.Title.Romaji;
                                entry.source = 0;
                                entry.url = "";
                                entry.Categories = new List<int>();
                                entry.Categories.Add(categoryId);
                                backupmangalist.Add(entry);
                                // Add entry for json
                                var jsonEntry = new BackupMangaJson();
                                jsonEntry.manga = new object[] { item.Media.Title.Romaji, item.Media.Title.Romaji, 0, 0, 0 };
                                jsonEntry.categories = new string[] { categoryName };
                                backupMangaJson.Add(jsonEntry);
                            }
                        }
                    }
                }
                tachilist.Clear(); // Micro optimization
                mangalist.Clear();
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
                        }
                    }
                    catch (Exception ex) {  Logs.Err(ex); }
                    // Serialize to json file
                    try
                    {
                        var backupTachiJson = new BackupTachiJson();
                        backupTachiJson.version = 2;
                        backupTachiJson.Mangas = backupMangaJson;
                        backupTachiJson.Categories.Add(new object[] { categoryName, categoryId });
                        string json = JsonConvert.SerializeObject(backupTachiJson, Formatting.Indented);
                        GlobalFunc.WriteFile(outputJson, json);
                    }
                    catch (Exception ex) { Logs.Err(ex); };
                }
            });
        }
        // ############################################################ End of Class
    }
}
