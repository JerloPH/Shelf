using System;
using System.IO;

namespace Shelf.Functions
{
    public static class Logs
    {
        public static void LogString(string file, string content)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter s = new StreamWriter(fs))
                    {
                        s.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]: {content}");
                        s.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public static void App(string content)
        {
            if (!String.IsNullOrWhiteSpace(content))
                LogString(GlobalFunc.FILE_LOG, content);
        }
        public static void Err(Exception ex)
        {
            if (ex != null)
                LogString(GlobalFunc.FILE_LOG_ERR, ex.ToString());
        }
    }
}
