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
using System.IO.Compression;
using System.Diagnostics;
using Shelf.Anilist;
using Microsoft.WindowsAPICodePack.Dialogs;
using Shelf.CustomEnums;
using JerloPH_CSharp;
using System.Reflection;
using System.ComponentModel;

namespace Shelf.Functions
{
    public static class GlobalFunc
    {
        public static string DIR_START = "";
        public static string DIR_RES = "";
        public static string DIR_DATA = "";
        public static string DIR_OUTPUT = "";
        public static string DIR_OUTPUT_ROOT = "";
        public static string DIR_TEMP = "";
        public static string DIR_TEMP_ANIMECOVER = "";
        public static string DIR_TEMP_MANGACOVER = "";
        public static string FILE_APPCONFIG = "";
        public static string FILE_ANILIST_CONFIG = "";
        public static string FILE_LOCAL_MEDIA = "";
        public static string FILE_ANIME = "";
        public static string FILE_MANGA = "";
        public static string FILE_LOG = "";
        public static string FILE_LOG_ERR = "";
        public static string FILE_LOG_DEBUG = "";
        public static string FILE_AUTH_CODE = "";
        public static string FILE_PUB_TKN = "";
        public static string DATE_TODAY = "";

        public static List<string> INCLUDED_STATUS { get; set; } = new List<string>();
        public static bool DEBUG { get; set; } = false;

        public static void InitializedApp()
        {
            try
            {
                if (Debugger.IsAttached)
                    DEBUG = true;

                DATE_TODAY = DateTime.Now.ToString("yyyy-MM-dd");
                DIR_START = AppContext.BaseDirectory;
                // Directories
                DIR_RES = FileHelper.CreateNewFolder(DIR_START, "Resources");
                DIR_DATA = FileHelper.CreateNewFolder(DIR_START, "data");
                DIR_OUTPUT_ROOT = FileHelper.CreateNewFolder(DIR_START, "output");
                DIR_OUTPUT = FileHelper.CreateNewFolder(DIR_OUTPUT_ROOT, DATE_TODAY);
                DIR_TEMP = FileHelper.CreateNewFolder(DIR_START, "temp");
                DIR_TEMP_ANIMECOVER = FileHelper.CreateNewFolder(DIR_TEMP, "coverAnime");
                DIR_TEMP_MANGACOVER = FileHelper.CreateNewFolder(DIR_TEMP, "coverManga");
                // Logs
                FILE_LOG = Path.Combine(DIR_START, "ShelfApp.log");
                FILE_LOG_ERR = Path.Combine(DIR_START, "ShelfApp_Error.log");
                FILE_LOG_DEBUG = Path.Combine(DIR_START, "Shelf_Debug.log");
                Logs.Initialize(DIR_START, FILE_LOG, FILE_LOG_ERR, FILE_LOG_DEBUG, true);
                Msg.Initialize(DIR_START, "Shelf");
                // Data files
                FILE_APPCONFIG = Path.Combine(DIR_DATA, "AppSettings.json");
                FILE_ANILIST_CONFIG = Path.Combine(DIR_DATA, "anilistConfig.json");
                FILE_ANIME = Path.Combine(DIR_DATA, "AnilistMediaANIME.json");
                FILE_MANGA = Path.Combine(DIR_DATA, "AnilistMediaMANGA.json");
                FILE_AUTH_CODE = Path.Combine(DIR_DATA, "AuthCode.tkn");
                FILE_PUB_TKN = Path.Combine(DIR_DATA, "PublicToken.tkn");
                FILE_LOCAL_MEDIA = Path.Combine(DIR_DATA, "LocalMediaPaths.json");
                AppSettings.LoadAppConfig(true);
            }
            catch (Exception ex) { Logs.Err(ex); Msg.ShowWarning("Some files are not initialized!"); }
            try
            {
                if (!File.Exists(FILE_ANILIST_CONFIG))
                    AnilistRequest.UpdateConfig("", "");
            }
            catch (Exception ex) { Logs.Err(ex); };
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
                Logs.Err(ex);
                return "0.0.1-alpha";
            }
        }
        public static string GetEnumDesc(Enum value)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }
            return "";
        }
        public static void ShowErr(string msg, Exception ex, Form parent)
        {
            Msg.ShowError("", msg, parent, ex, false);
        }
        #region File IO
        public static bool WriteObjectToJson(string file, object data)
        {
            try
            {
                string content = JsonConvert.SerializeObject(data, Formatting.Indented);
                FileHelper.WriteFile(file, content);
                return true;
            }
            catch (Exception ex) { Logs.Err(ex); }
            return false;
        }
        public static bool WriteMediaJsonToFile(string Media, AnilistAnimeManga mediajson)
        {
            if (mediajson != null)
            {
                try
                {
                    string file = Media.Equals("ANIME") ? FILE_ANIME : FILE_MANGA;
                    return JsonEncode(mediajson, file);
                }
                catch { throw; }
            }
            return false;
        }
        public static AnilistAnimeManga ReadMediaJson(string file)
        {
            AnilistAnimeManga media = null;
            try
            {
                string content = FileHelper.ReadFromFile(file);
                if (!String.IsNullOrWhiteSpace(content))
                    media = JsonConvert.DeserializeObject<AnilistAnimeManga>(content);
            }
            catch (Exception ex) { Logs.Err(ex); }
            return media;
        }
        public static T JsonDecode<T>(string file) where T : class
        {
            if (!File.Exists(file))
                throw new Exception($"Json file does not exist! File: {file}");
            try
            {
                string content = FileHelper.ReadFromFile(file);
                if (!String.IsNullOrWhiteSpace(content))
                {
                    var media = JsonConvert.DeserializeObject<T>(content);
                    return media;
                }
            }
            catch { throw; }
            return null;
        }
        public static bool JsonEncode<T>(T data, string file) where T : class
        {
            if (null == data) return false;
            try
            {
                return FileHelper.WriteFile(file, JsonConvert.SerializeObject(data, Formatting.Indented));
            }
            catch  { throw; }
        }
        public static byte[] ProtoSerialize<T>(T record) where T : class
        {
            if (null == record) return null;
            try
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, record);
                    return stream.ToArray();
                }
            }
            catch { throw; }
        }
        
        public static string BrowseForFile(string Title, string filter, string InitialDir)
        {
            string ret = "";
            OpenFileDialog selectFile = new OpenFileDialog
            {
                InitialDirectory = Directory.Exists(InitialDir) ? InitialDir : DIR_START,
                Filter = filter,
                Title = Title,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                Multiselect = false
            };
            selectFile.ShowDialog();
            if (String.IsNullOrWhiteSpace(selectFile.FileName) == false)
            {
                ret = selectFile.FileName;
            }
            selectFile.Dispose();
            return ret;
        }
        public static string BrowseForDirectory(string title, string initialDir, string initialVal, SettingType setType)
        {
            string ret = initialVal;
            try
            {
                using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
                {
                    dialog.Title = title;
                    dialog.Multiselect = false;
                    dialog.InitialDirectory = initialDir;
                    dialog.IsFolderPicker = setType == SettingType.Directory;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        if (!String.IsNullOrWhiteSpace(dialog.FileName))
                            ret = dialog.FileName;
                    }
                }
            }
            catch { throw; }
            return ret;
        }
        public static bool FileOpeninExplorer(string filePath, bool isSelected)
        {
            try
            {
                if (isSelected)
                    Process.Start("explorer.exe", @"/select," + $"{ filePath }" + '"');
                else
                    Process.Start("explorer.exe", filePath);

                return true;
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
                Msg.ShowWarning("Cannot Browse to file!");
            }
            return false;
        }
        public static List<string> SearchFoldersFromDirectory(string sDir)
        {
            List<string> list = new List<string>();
            string directory = sDir.TrimEnd('\\');
            try
            {
                foreach (string foldertoAdd in Directory.GetDirectories(directory))
                {
                    if (Directory.Exists(foldertoAdd))
                    {
                        list.Add(foldertoAdd); // Add to list
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
            }
            return list;
        }
        #endregion
        #region Messages
        #endregion
        // ############################################################ End of Class
    }
}
