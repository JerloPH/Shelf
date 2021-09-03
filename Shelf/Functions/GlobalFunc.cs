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
    public static class GlobalFunc
    {
        public static string DIR_OUTPUT = "";
        public static string FILE_ANIME = "";
        public static string FILE_MANGA = "";
        public static string FILE_LOG = "";
        public static string FILE_LOG_ERR = "";

        public static void InitializedApp()
        {
            try
            {
                FILE_LOG = Path.Combine(AppContext.BaseDirectory, "ShelfApp.log");
                FILE_LOG_ERR = Path.Combine(AppContext.BaseDirectory, "ShelfApp_Error.log");
                FILE_ANIME = Path.Combine(AppContext.BaseDirectory, "AnilistMediaANIME.json");
                FILE_MANGA = Path.Combine(AppContext.BaseDirectory, "AnilistMediaMANGA.json");
                
                DIR_OUTPUT = Path.Combine(AppContext.BaseDirectory, "output");
                Directory.CreateDirectory(DIR_OUTPUT);
            }
            catch (Exception ex) { Logs.Err(ex); }
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
        public static bool WriteMediaJsonToFile(string Media, AnilistAnimeManga mediajson)
        {
            if (mediajson != null)
            {
                try
                {
                    string file = Media.Equals("ANIME") ? FILE_ANIME : FILE_MANGA;
                    return WriteFile(file, JsonConvert.SerializeObject(mediajson));
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
