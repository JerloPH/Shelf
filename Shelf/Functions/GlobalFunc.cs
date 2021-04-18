using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.Functions
{
    public static class GlobalFunc
    {
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
        #endregion
    }
}
