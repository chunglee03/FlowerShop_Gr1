using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace adminFlowerShop_Gr1.Extension
{
    public static class HashMD5
    {
        public static string ToMD5(this string str)
        {
            MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
            byte[] bHash = mD5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bHash) 
                sbHash.Append(string.Format("{0:x2}",b));
            return sbHash.ToString();
        }
    }
}
