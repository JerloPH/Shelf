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
using Shelf.Enum;

namespace Shelf.Functions
{
    public static class GlobalFunc
    {
        public static string DIR_START = "";
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
                DIR_DATA = CreateNewFolder(DIR_START, "data");
                DIR_OUTPUT_ROOT = CreateNewFolder(DIR_START, "output");
                DIR_OUTPUT = CreateNewFolder(DIR_OUTPUT_ROOT, DATE_TODAY);
                DIR_TEMP = CreateNewFolder(DIR_START, "temp");
                DIR_TEMP_ANIMECOVER = CreateNewFolder(DIR_TEMP, "coverAnime");
                DIR_TEMP_MANGACOVER = CreateNewFolder(DIR_TEMP, "coverManga");
                // Logs
                FILE_LOG = Path.Combine(DIR_START, "ShelfApp.log");
                FILE_LOG_ERR = Path.Combine(DIR_START, "ShelfApp_Error.log");
                FILE_LOG_DEBUG = Path.Combine(DIR_START, "Shelf_Debug.log");
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
            catch (Exception ex) { Logs.Err(ex); GlobalFunc.Alert("Some files are not initialized!"); }
            try
            {
                if (!File.Exists(FILE_ANILIST_CONFIG))
                    AnilistRequest.UpdateConfig("", "");
            }
            catch (Exception ex) { Logs.Err(ex); };
        }
        public static string CreateNewFolder(string root, string folderName)
        {
            try
            {
                string dir = Path.Combine(root, folderName);
                Directory.CreateDirectory(dir);
                return dir;
            }
            catch { throw; }
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
            catch (Exception ex) { Logs.Err(ex); }
            return content;
        }
        public static bool WriteFile(string filename, string content)
        {
            try
            {
                if (File.Exists(filename))
                    File.Delete(filename);
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
            catch (Exception ex) { Logs.Err(ex); }
            return false;
        }
        public static void AppendFile(string file, string content, bool Addline=false)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter s = new StreamWriter(fs))
                    {
                        s.Write(content + (Addline ? "\n" : ""));
                        s.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        public static void PrependFile(string file, string content)
        {
            string prev = ReadFromFile(file);
            WriteFile(file, content);
            AppendFile(file, prev);
        }
        public static bool WriteObjectToJson(string file, object data)
        {
            try
            {
                string content = JsonConvert.SerializeObject(data, Formatting.Indented);
                WriteFile(file, content);
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
                    return WriteFile(file, JsonConvert.SerializeObject(mediajson, Formatting.Indented));
                }
                catch (Exception ex) { Logs.Err(ex); }
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
            catch (Exception ex) { Logs.Err(ex); }
            return media;
        }
        public static T JsonDecode<T>(string file) where T : class
        {
            if (!File.Exists(file))
                throw new Exception($"Json file does not exist! File: {file}");
            try
            {
                string content = ReadFromFile(file);
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
                return WriteFile(file, JsonConvert.SerializeObject(data, Formatting.Indented));
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
        public static void Compress(string filepath)
        {
            try
            {
                FileInfo fileToCompress = new FileInfo(filepath);
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }
                        //FileInfo info = new FileInfo(filepath + ".gz");
                        //Logs.App($"Compressed {fileToCompress.Name} from {fileToCompress.Length} to {info.Length} bytes.");
                    }
                }
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        public static string Decompress(string filepath, string newFileName)
        {
            try
            {
                if (File.Exists(newFileName))
                {
                    File.Delete(newFileName);
                }
                FileInfo fileToDecompress = new FileInfo(filepath);
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    using (FileStream file = File.Create(newFileName))
                    {
                        using (GZipStream decStream = new GZipStream(originalFileStream, CompressionMode.Decompress, false))
                        {
                            decStream.CopyTo(file);
                            //Console.WriteLine($"Decompressed: {file.Name}");
                        }
                        file.SetLength(file.Position);
                        file.Close();
                    }
                    originalFileStream.Close();
                }
                if (File.Exists(newFileName))
                    return newFileName;
            }
            catch (Exception ex) { Logs.Err(ex); }
            return "";
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
        public static bool FileOpeninExplorer(string filePath)
        {
            try
            {
                Process.Start("explorer.exe", @"/select," + $"{ filePath }" + '"');
                return true;
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
                Alert("Cannot Browse to file!");
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
        // ############################################################ End of Class
    }
}
