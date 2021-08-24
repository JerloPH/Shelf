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
                catch (Exception ex) { };
            });
        }
        public static async Task GenerateTachiBackup(string file)
        {
            var tachilist = new List<long>();
            await Task.Run((Action)async delegate
            {
                try
                {
                    string output = file + "_output.json";
                    string value = GlobalFunc.ReadFromFile(file);
                    using (var ms = File.OpenRead(file))
                    {
                        ms.Position = 0;
                        var tachi = Serializer.Deserialize<BackupTachi>(ms);
                        foreach (var item in tachi.Main)
                        {
                            GlobalFunc.AppendFile(file, item.Title);
                        }
                    }

                    //using (var input = File.OpenRead(file))
                    //{
                    //    string value = ReadFromFile(file);
                    //    string jsonString = null;
                    //    message.MergeFrom(input); //read message from protodat file
                    //    JsonFormatter formater = new JsonFormatter(
                    //        new JsonFormatter.Settings(false));
                    //    jsonString = formater.Format(message);
                    //    WriteFile(output, jsonString);
                    //}

                    //using (var ms = new MemoryStream(File.ReadAllBytes(file)))
                    //{
                    //    //Serializer.Serialize(ms, new BackupTachi());
                    //    ms.Position = 0;
                    //    var tachi = Serializer.Deserialize<BackupTachi>(ms);
                    //    if (tachi != null)
                    //    {
                    //        foreach (var item in tachi.Main)
                    //        {
                    //            AppendFile(file, item.Title);
                    //        }
                    //    }
                    //}

                    //using (var ms = new MemoryStream(File.ReadAllBytes(file)))
                    //{
                    //    Serializer.Serialize(ms, new BackupManga());
                    //    ms.Position = 0;
                    //    var tachi = Serializer.Deserialize<BackupManga>(ms);

                    //    //string json = JsonConvert.SerializeObject(tachi);
                    //    var buffer = ms.GetBuffer();
                    //    var jsonString = Convert.ToBase64String(buffer, 0, (int)ms.Length);
                    //    WriteFile(file + "_output.json", jsonString);
                    //}
                }
                catch (Exception ex) { Logs.App(ex.ToString()); GlobalFunc.Alert("Error loading proto file!"); }
                //var manga = await GetMangaList();
                //foreach (var item in manga)
                //{
                //}
            });
        }
    }
}
