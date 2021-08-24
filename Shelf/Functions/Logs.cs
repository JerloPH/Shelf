using System;
using System.IO;

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
            catch (Exception ex) { }
        }
    }
}
