using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Shelf.Json;

namespace Shelf.Functions
{
    public static class GlobalFunc
    {
        #region File IO
        public static bool WriteMediaJsonToFile(string Media, AnilistAnimeManga mediajson)
        {
            try
            {
                if (mediajson != null)
                    return WriteFile($"AnilistMedia{Media}.json", JsonConvert.SerializeObject(mediajson));
            }
            catch { }
            return false;
        }
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
