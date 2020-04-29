using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CSVDynamicParser.Web
{
    public static class VersionUtils
    {
        public static Dictionary<string, string> FileHashDic = new Dictionary<string, string>();
        public static string GetFileVersion(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            if (FileHashDic.ContainsKey(fileName))
            {
                return FileHashDic[fileName];
            }
            else
            {
                string hashvalue = GetFileShaHash(filePath); 
                FileHashDic.Add(fileName, hashvalue);
                return hashvalue;
            }
        }

        private static string GetFileShaHash(string filePath)
        {
            string hashSHA1 = String.Empty;
            if (System.IO.File.Exists(filePath))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    System.Security.Cryptography.SHA1 calculator = System.Security.Cryptography.SHA1.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashSHA1 = stringBuilder.ToString();
                }
            }
            return hashSHA1;
        }
    }
}