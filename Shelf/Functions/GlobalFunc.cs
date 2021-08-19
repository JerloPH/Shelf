using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Shelf.Json;
using Shelf.Entity;
using ProtoBuf;
using ProtoBuf.Meta;
using ProtoBuf.Serializers;
using ProtoBuf.WellKnownTypes;
using Google.Protobuf;

namespace Shelf.Functions
{
    public static class Logs
    {
        public static void App(string content)
        {
            try
            {
                using (FileStream fs = new FileStream(GlobalFunc.FILE_LOG, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter s = new StreamWriter(fs))
                    {
                        s.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]: {content}");
                        s.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception) { }
        }
    }
    public static class GlobalFunc
    {
        public static string DIR_OUTPUT = "";
        public static string FILE_ANIME = "";
        public static string FILE_MANGA = "";
        public static string FILE_LOG = "";

        public static void InitializedApp()
        {
            try
            {
                FILE_LOG = Path.Combine(AppContext.BaseDirectory, "ShelfApp.log");
                FILE_ANIME = Path.Combine(AppContext.BaseDirectory, "AnilistMediaANIME.json");
                FILE_MANGA = Path.Combine(AppContext.BaseDirectory, "AnilistMediaMANGA.json");
                
                DIR_OUTPUT = Path.Combine(AppContext.BaseDirectory, "output");
                Directory.CreateDirectory(DIR_OUTPUT);
            }
            catch (Exception ex) { }
        }
        public static string GetAppVersion()
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
            catch (Exception ex)
            {
                return "1.0.0.0-alpha";
            }
        }
        #region File IO
        public static string ReadFromFile(string filename)
        {
            string content = String.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    content = sr.ReadToEnd();
                }
            }
            catch { }
            return content;
        }
        public static bool WriteFile(string filename, string content)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch { throw new Exception("Cannot overwrite existing file!"); }
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(content);
                }
                return true;
            }
            catch { }
            return false;
        }
        public static void AppendFile(string file, string content)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter s = new StreamWriter(fs))
                    {
                        s.Write(content);
                        s.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception ex) { }
        }
        public static void PrependFile(string file, string content)
        {
            string prev = ReadFromFile(file);
            WriteFile(file, content);
            AppendFile(file, prev);
        }
        public static bool WriteMediaJsonToFile(string Media, AnilistAnimeManga mediajson)
        {
            if (mediajson != null)
            {
                try
                {
                    string file = Media.Equals("ANIME") ? FILE_ANIME : FILE_MANGA;
                    return WriteFile(file, JsonConvert.SerializeObject(mediajson));
                }
                catch (Exception ex) { }
            }
            return false;
        }
        public static AnilistAnimeManga ReadMediaJson(string file)
        {
            AnilistAnimeManga media = null;
            try
            {
                string content = ReadFromFile(file);
                if (!String.IsNullOrWhiteSpace(content))
                    media = JsonConvert.DeserializeObject<AnilistAnimeManga>(content);
            }
            catch (Exception ex) { }
            return media;
        }
        #endregion
        #region Tasks
        public async static Task<List<Entry>> GetMediaList(string file)
        {
            var result = new List<Entry>(); // resulting list
            var idList = new List<long>(); // already existing entries
            await Task.Run((Action)delegate
            {
                var mediaJson = ReadMediaJson(file);
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
            return await GetMediaList(FILE_ANIME);
        }
        public static async Task<List<Entry>> GetMangaList()
        {
            return await GetMediaList(FILE_MANGA);
        }
        public static async Task ProcessMedia(List<Entry> medialist, string media, string outputfile, string username)
        {
            string xmltoWrite = "";
            string AnilistStatus = "";
            string prepend = "";
            var count = new StatusCount();
            await Task.Run((Action)delegate
            {
                foreach (var item in medialist)
                {
                    if (item.Media.IdMal != null)
                    {
                        xmltoWrite = MAL.XmlMedia(media, item);
                        AppendFile(outputfile, xmltoWrite);

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
                }
                // Append finalizer
                xmltoWrite = $"</my{media}list>";
                AppendFile(outputfile, xmltoWrite);
                // Prepend 'myinfo' tree
                prepend = MAL.PrependInfo(media, username, count);
                PrependFile(outputfile, prepend);
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
                    string value = ReadFromFile(file);
                    using (var ms = File.OpenRead(file))
                    {
                        ms.Position = 0;
                        var tachi = Serializer.Deserialize<BackupTachi>(ms);
                        foreach (var item in tachi.Main)
                        {
                            AppendFile(file, item.Title);
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
                catch (Exception ex) { Logs.App(ex.ToString()); Alert("Error loading proto file!"); }
                //var manga = await GetMangaList();
                //foreach (var item in manga)
                //{
                //}
            });
        }
        #endregion
        #region Messages
        public static DialogResult Alert(string message, string caption, Form parent)
        {
            if (!String.IsNullOrWhiteSpace(caption))
                caption = "Shelf";

            return MessageBox.Show(parent, message, caption);
        }
        public static DialogResult Alert(string message, Form parent)
        {
            return Alert(message, "", parent);
        }
        public static DialogResult Alert(string message)
        {
            return Alert(message, "", null);
        }
        #endregion
    }
}
